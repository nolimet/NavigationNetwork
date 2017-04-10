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
            throw new System.NotImplementedException();
        }

        public void OpenBuildMenu()
        {
            buildMenu.SetActive(true);
        }

        public void CloseBuildMenu()
        {
            buildMenu.SetActive(false);
        }

        public void PlaceTower(Towers tower)
        {
            Utils.TowerIdentifyable o =
            towers.First(x => x.type == tower);

            GameObject newTower = Instantiate(o.Tower);

            PlacementManager.Place(newTower, true);
        }

    }
}