using UnityEngine;
using System.Threading;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
namespace NavigationNetwork
{
    public class NavigationNetworkControler : MonoBehaviour
    {
        public static NavigationNetworkControler instance
        {
            get
            {
                if (_instance)
                    return _instance;

                _instance = FindObjectOfType<NavigationNetworkControler>();

                if (_instance)
                    return _instance;

                GameObject controler = Instantiate(Resources.Load("EnergyNetControler"), Vector3.zero, Quaternion.identity) as GameObject;
                _instance = controler.GetComponent<NavigationNetworkControler>();
                _instance.Start();
                return _instance;
            }
        }

        private static NavigationNetworkControler _instance;

        private float CallculedWaitTime = 1f;
        public int TicksPerSecond = 4;

        int _tps = 0;
        public int tps { get { return _tps; } }
        float timer = 0f;

        List<NavigationNode> nodes = new List<NavigationNode>();
        List<NavigatorSpawnPoint> Spawners = new List<NavigatorSpawnPoint>();

        List<Vector3> CurrentnodesPos = new List<Vector3>();
        List<Vector3> LastFrameNodePos = new List<Vector3>();

        public delegate void NetUpdate(List<NavigationNode> node);
        public static event NetUpdate OnNetUpdate;
        public delegate void VoidDelegate();
        public static event VoidDelegate OnRebuild;
        public static event VoidDelegate OnPowerSend;
        public static event VoidDelegate OnPullUpdate;

        float[] tpsar = new float[5];

        Thread GridBuilder;

        public float GridBuildTime { get; private set; }
        public float GridUpdateTime { get; private set; }
        public bool ThreadActive { get { return GridBuilder.IsAlive; } }

        #region Start and Updates
        void Awake()
        {
            _instance = this;
        }

        public void Start()
        {
            if (!NavUtil.createdPackageParent)
            {
                NavUtil.createdPackageParent = true;
                GameObject ep = new GameObject();
                ep.name = "--EnergyPackages";
                NavUtil.packageParent = ep.transform;
            }

            name = "--NetworkControler";
            StartCoroutine("CheckForChanges");
            
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                GridBuilder = new Thread(RangeCheckThread);
                GridBuilder.Start();
            }
            else
            {
                StartCoroutine("RangeCheck");
            }
        }

        public void Stop()
        {
            StopCoroutine("CheckForChanges");
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                GridBuilder.Abort();
            }
            else
            {
                StopCoroutine("RangeCheck");
            }
        }

        void Update()
        {
            CallculedWaitTime = 1f / TicksPerSecond;

            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                tpsar[4] = tpsar[3];
                tpsar[3] = tpsar[2];
                tpsar[2] = tpsar[1];
                tpsar[1] = tpsar[0];
                tpsar[0] = _tps;
                _tps = 0;
                timer = 0;
                NavUtil.RealTPS = (tpsar[4] + tpsar[3] + tpsar[2] + tpsar[1] + tpsar[0]) / 5f;
            }


        }
        #endregion

        #region IEnumerators

        //checks if grid changed and rebuilds if needed;
        //Basicly everything that could not be off loaded to a other thread
        IEnumerator CheckForChanges()
        {
            Debug.Log(name + "Started Checker");

            System.DateTime startTime = System.DateTime.Now; ;
            GridListBuilder();

            GridBuildTime = (float)(System.DateTime.Now - startTime).TotalMilliseconds;

            int ticksPast = 0;
            while (Application.isPlaying)
            {
               
                if (NavUtil.LastNetworkObjectCount != NavUtil.CurrentNetworkObjects)
                {
                     startTime = System.DateTime.Now;
                    GridListBuilder();
                    if ((System.DateTime.Now - startTime).TotalMilliseconds > 0) 
                    GridBuildTime = (float)(System.DateTime.Now - startTime).TotalMilliseconds;
                }

                if (OnPowerSend != null && ticksPast >= 5)
                    OnPowerSend();

                if (ticksPast >= 5)
                    ticksPast = 0;
                _tps++;
                ticksPast++;

                
                yield return new WaitForSeconds(0.05f);
            }
        }

        /// <summary>
        /// MultiThreading function handels all Multi Threading functions
        /// </summary>
        void RangeCheckThread()
        {
            List<NavigationNode> tmpNodeList;

            System.DateTime startTime;
            while (true)
            {
                startTime = System.DateTime.Now;
                tmpNodeList = nodes.Select(x => x).ToList();
                
                try
                {

                    //if (EnergyGlobals.LastNetworkObjectCount != EnergyGlobals.CurrentNetworkObjects)
                    //   UpdateGride();
                    LastFrameNodePos = CurrentnodesPos;
                    CurrentnodesPos = new List<Vector3>();
                    foreach (NavigationNode node in tmpNodeList)
                    {
                        //node.GetInRangeNodes(nodes);
                        CurrentnodesPos.Add(node.position);
                    }

                    if (OnNetUpdate != null)
                        OnNetUpdate(tmpNodeList);
                    if (OnPullUpdate != null)
                        OnPullUpdate();
                    if (OnRebuild != null && !CheckNodepos())
                        OnRebuild();


                        GridUpdateTime = (float)(System.DateTime.Now - startTime).TotalMilliseconds; ;
                    Thread.Sleep(10);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error in GridBuilder " + e.Message);
                    Debug.LogException(e);
                    throw;
                }
                
            }
        }

        /// <summary>
        /// Alternative to multithreading if platfrom does not support it
        /// </summary>
        IEnumerator RangeCheck()
        {
            System.DateTime startTime;
            Debug.Log(name + "Started RangeCheck");

            while (Application.isPlaying)
            {
                startTime = System.DateTime.Now;
                //if (EnergyGlobals.LastNetworkObjectCount != EnergyGlobals.CurrentNetworkObjects)
                //   UpdateGride();
                LastFrameNodePos = CurrentnodesPos;
                CurrentnodesPos = new List<Vector3>();
                foreach (NavigationNode node in nodes)
                {
                    //node.GetInRangeNodes(nodes);
                    CurrentnodesPos.Add(node.transform.position);
                }

                /*foreach (EnergyGenator gen in generators)
                {
                    gen.GetInRangeNodes(nodes);
                }*/
                if (OnNetUpdate != null)
                    OnNetUpdate(nodes);

                if (OnPullUpdate != null)
                    OnPullUpdate();

                if (OnRebuild != null && !CheckNodepos())
                    OnRebuild();

                
                GridUpdateTime = (float)(System.DateTime.Now - startTime).TotalMilliseconds;
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion

        #region misc functions
        //public void OnApplicationQuit()
        //{
        //    GridBuilder.Abort();

        //}

        bool CheckNodepos()
        {
            if (CurrentnodesPos.Count != LastFrameNodePos.Count)
                return false;
            int l = CurrentnodesPos.Count;
            for (int i = 0; i < l; i++)
            {
                if (CurrentnodesPos[i] != LastFrameNodePos[i])
                    return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Sorts out all the objects that are registerd  by the networkControler
        /// </summary>
        public void GridListBuilder()
        {
            //Debug.Log(this.name + ": Updated Grid");
            nodes = new List<NavigationNode>();
            Spawners = new List<NavigatorSpawnPoint>();
            foreach (GameObject go in NavUtil.NetWorkObjects)
            {
                if (go != null)
                {
                    if (go.tag == NavTags.EnergyNode)
                    {
                        NavigationNode tmp = go.GetComponent<NavigationNode>();
                        nodes.Add(tmp);
                    }
                    else if (go.tag == NavTags.EnergyGenartor)
                    {
                        NavigatorSpawnPoint tmp = go.GetComponent<NavigatorSpawnPoint>();
                        Spawners.Add(tmp);
                    }
                }
            }
            NavUtil.LastNetworkObjectCount = NavUtil.CurrentNetworkObjects;

        }
    }
}
