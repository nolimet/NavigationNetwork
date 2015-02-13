using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace EnergyNet
{
    public class EnergyNetWorkControler : MonoBehaviour
    {
        private float CallculedWaitTime = 1f;
        public int TicksPerSecond = 4;

        int tps = 0;
        float timer = 0f;

        List<EnergyNode> nodes = new List<EnergyNode>();
        List<EnergyGenator> generators = new List<EnergyGenator>();

        List<Vector3> CurrentnodesPos = new List<Vector3>();
        List<Vector3> LastFrameNodePos = new List<Vector3>();

        public delegate void NetUpdate(List<EnergyNode> node);
        public static event NetUpdate OnNetUpdate;
        public delegate void NetWorkRebuild();
        public static event NetWorkRebuild OnRebuild;

        float[] tpsar = new float[5];

        #region Start and Updates
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

            tpsar[4] = 20;
            tpsar[3] = 20;
            tpsar[2] = 20;
            tpsar[1] = 20;
            tpsar[0] = 20;
        }

        public void Stop()
        {
            StopCoroutine("CheckForChanges");
            StopCoroutine("RangeCheck");
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
                tpsar[0] = tps;
                tps = 0;
                timer = 0;
                EnergyGlobals.RealTPS = (tpsar[4] + tpsar[3] + tpsar[2] + tpsar[1] + tpsar[0]) / 5f;
            }
        }
        #endregion

        #region IEnumerators
        IEnumerator CheckForChanges()
        {
            UpdateGride();
            Debug.Log(name + "Started Checker");
            int ticksPast = 0;
            while (Application.isPlaying)
            {
                if (EnergyGlobals.LastNetworkObjectCount != EnergyGlobals.CurrentNetworkObjects)
                    UpdateGride();
                
                    foreach (EnergyNode node in nodes)
                    {
                        if (ticksPast >= 5)
                            node.sendPower();
                        node.GetPull();
                        
                    }

                foreach (EnergyGenator gen in generators)
                {
                    if (ticksPast >= 5)
                        gen.sendPowerV2();
                    gen.Genarate();
                }

                if (ticksPast >= 5)
                    ticksPast = 0;
                tps++;
                ticksPast++;
                yield return new WaitForSeconds(CallculedWaitTime);
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
                if(OnNetUpdate!=null)
                    OnNetUpdate(nodes);
                if (OnRebuild != null && !CheckNodepos())
                    OnRebuild();
                yield return new WaitForSeconds(CallculedWaitTime * 2f);
            }
        }
        #endregion

        #region misc functions
        void OnApplicationStop()
        {
            StopCoroutine("CheckForChanges");
        }

        static public EnergyNetWorkControler GetThis()
        {
            EnergyNetWorkControler output = null;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                if (go.name == "--NetworkControler")
                {
                    output = go.GetComponent<EnergyNetWorkControler>();
                    return output;
                }
            }
            if (output != null)
                return output;
            else
            {
                GameObject controler = Instantiate(Resources.Load("EnergyNetControler"), Vector3.zero, Quaternion.identity) as GameObject;
                output = controler.GetComponent<EnergyNetWorkControler>();
                output.Start();
                return output;
            }
        }

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
        public void UpdateGride()
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
