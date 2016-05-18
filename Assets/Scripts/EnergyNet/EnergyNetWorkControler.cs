using UnityEngine;
using System.Threading;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
namespace EnergyNet
{
    public class EnergyNetWorkControler : MonoBehaviour
    {
        public static EnergyNetWorkControler instance
        {
            get
            {
                if (_instance)
                    return _instance;

                _instance = FindObjectOfType<EnergyNetWorkControler>();

                if (_instance)
                    return _instance;

                GameObject controler = Instantiate(Resources.Load("EnergyNetControler"), Vector3.zero, Quaternion.identity) as GameObject;
                _instance = controler.GetComponent<EnergyNetWorkControler>();
                _instance.Start();
                return _instance;
            }
        }

        private static EnergyNetWorkControler _instance;

        private float CallculedWaitTime = 1f;
        public int TicksPerSecond = 4;

        int _tps = 0;
        public int tps { get { return _tps; } }
        float timer = 0f;

        List<EnergyNode> nodes = new List<EnergyNode>();
        List<EnergyGenator> generators = new List<EnergyGenator>();

        List<Vector3> CurrentnodesPos = new List<Vector3>();
        List<Vector3> LastFrameNodePos = new List<Vector3>();

        public delegate void NetUpdate(List<EnergyNode> node);
        public static event NetUpdate OnNetUpdate;
        public delegate void VoidDelegate();
        public static event VoidDelegate OnRebuild;
        public static event VoidDelegate OnPowerSend;
        public static event VoidDelegate OnPullUpdate;

        float[] tpsar = new float[5];

        //Thread GridBuilder;

        #region Start and Updates
        void Awake()
        {
            _instance = this;
        }

        public void Start()
        {
            if (!EnergyGlobals.createdPackageParent)
            {
                EnergyGlobals.createdPackageParent = true;
                GameObject ep = new GameObject();
                ep.name = "--EnergyPackages";
                EnergyGlobals.packageParent = ep.transform;
            }

            name = "--NetworkControler";
            StartCoroutine("CheckForChanges");
            StartCoroutine("RangeCheck");
            //GridBuilder = new Thread(RangeCheckThread);
            //GridBuilder.Start();
        }

        public void Stop()
        {
            StopCoroutine("CheckForChanges");
            StopCoroutine("RangeCheck");
            //GridBuilder.Abort();
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
                EnergyGlobals.RealTPS = (tpsar[4] + tpsar[3] + tpsar[2] + tpsar[1] + tpsar[0]) / 5f;
            }


        }
        #endregion

        #region IEnumerators
        IEnumerator CheckForChanges()
        {
            GridListBuilder();
            Debug.Log(name + "Started Checker");
            int ticksPast = 0;
            while (Application.isPlaying)
            {
                if (EnergyGlobals.LastNetworkObjectCount != EnergyGlobals.CurrentNetworkObjects)
                    GridListBuilder();

                if (OnPowerSend != null && ticksPast >= 5)
                    OnPowerSend();


                /*  foreach (EnergyNode node in nodes)
                  {
                      if (ticksPast >= 5)
                          node.sendPower();
                      node.GetPull();

                  }*/

                /*foreach (EnergyGenator gen in generators)
                {
                    if (ticksPast >= 5)
                        gen.sendPowerV2();
                    gen.Genarate();
                }
                */
                if (ticksPast >= 5)
                    ticksPast = 0;
                _tps++;
                ticksPast++;
                yield return new WaitForSeconds(0.05f);
            }
        }

        void RangeCheckThread()
        {
            List<EnergyNode> tmpNodeList;

            while (true)
            {
                tmpNodeList = nodes.Select(x => x).ToList();
                try
                {
                    Debug.Log("jobStart");
                    //if (EnergyGlobals.LastNetworkObjectCount != EnergyGlobals.CurrentNetworkObjects)
                    //   UpdateGride();
                    LastFrameNodePos = CurrentnodesPos;
                    CurrentnodesPos = new List<Vector3>();
                    foreach (EnergyNode node in tmpNodeList)
                    {
                        //node.GetInRangeNodes(nodes);
                        CurrentnodesPos.Add(node.position);
                    }

                    /*foreach (EnergyGenator gen in generators)
                    {
                        gen.GetInRangeNodes(nodes);
                    }*/

                    if (OnNetUpdate != null)
                        OnNetUpdate(tmpNodeList);
                    if (OnPullUpdate != null)
                        OnPullUpdate();
                    if (OnRebuild != null && !CheckNodepos())
                        OnRebuild();

                    Thread.Sleep(10);
                    Debug.Log("jobDone");
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error in GridBuilder " + e.Message);
                    Debug.LogException(e);
                    throw;
                }

            }
        }

        IEnumerator RangeCheck()
        {
            Debug.Log(name + "Started RangeCheck");
            while (Application.isPlaying)
            {
                //if (EnergyGlobals.LastNetworkObjectCount != EnergyGlobals.CurrentNetworkObjects)
                //   UpdateGride();
                LastFrameNodePos = CurrentnodesPos;
                CurrentnodesPos = new List<Vector3>();
                foreach (EnergyNode node in nodes)
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
                yield return new WaitForSeconds(0.1f);
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

        #region enum Functions
        public void GridListBuilder()
        {
            //Debug.Log(this.name + ": Updated Grid");
            nodes = new List<EnergyNode>();
            generators = new List<EnergyGenator>();
            foreach (GameObject go in EnergyGlobals.NetWorkObjects)
            {
                if (go != null)
                {
                    if (go.tag == EnergyTags.EnergyNode)
                    {
                        EnergyNode tmp = go.GetComponent<EnergyNode>();
                        nodes.Add(tmp);
                    }
                    else if (go.tag == EnergyTags.EnergyGenartor)
                    {
                        EnergyGenator tmp = go.GetComponent<EnergyGenator>();
                        generators.Add(tmp);
                    }
                }
            }
            EnergyGlobals.LastNetworkObjectCount = EnergyGlobals.CurrentNetworkObjects;

        }
        #endregion
    }
}
