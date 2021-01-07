using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace TowerDefence.Managers
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager instance
        {
            get
            {
                if (_instance)
                    return _instance;
                _instance = FindObjectOfType<BuildManager>();
                if (_instance)
                    return _instance;

                Debug.LogError("NO BUILDMANAGER FOUND! Check what is calling it");
                return null;
            }
        }
        static BuildManager _instance;


        [SerializeField]
        private GameObject buildMenu;

        [SerializeField]
        private List<Utils.TowerIdentifyable> towers;

        private Utils.TowerIdentifyable CurrentTowerPlace; // The tower that is being placed at the moment

        public void Start()
        {
            PlacementManager.instance.onStartPlacing += Instance_onStartPlacing;
            PlacementManager.instance.onEndPlacing += Instance_onEndPlacing;
            PlacementManager.instance.onSuccesfullPlacement += Instance_onSuccesfullPlacement;
        }

        private void Instance_onSuccesfullPlacement()
        {
            ResourceManager.Buy(CurrentTowerPlace.Cost);
        }

        private void Instance_onEndPlacing()
        {
            CurrentTowerPlace = new Utils.TowerIdentifyable();
        }

        private void Instance_onStartPlacing()
        {
            CloseBuildMenu();
        }

        public void OpenBuildMenu()
        {
            buildMenu.SetActive(true);
        }

        public void ToggleBuildMenu()
        {
            if (buildMenu.activeSelf)
            {
                CloseBuildMenu();
            }
            else
            {
                OpenBuildMenu();
            }
        }

        public void CloseBuildMenu()
        {
            buildMenu.SetActive(false);
        }

        /// <summary>
        /// Places a tower based on interger given
        /// </summary>
        /// <param name="i">Tower given as interger</param>
        public void PlaceTower(int i)
        {
            Debug.Log(i);
            Towers t = (Towers)i;
            Utils.TowerIdentifyable o =
            towers.First(x => x.type == t);

            GameObject nt = Instantiate(o.Tower);
            nt.transform.position -= new Vector3(0, 0, nt.transform.position.z);
            PlacementManager.Place(nt, true);
        }

    }
}