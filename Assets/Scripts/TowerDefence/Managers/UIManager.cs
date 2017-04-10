using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TowerDefence.Utils;
namespace TowerDefence.Managers
{
    public class UIManager : MonoBehaviour
    {
        //UIStates
        [SerializeField]
        GameObject UIMainBody;
        [SerializeField]
        GameObject UIBuildingState;
        [SerializeField]
        GameObject UIEditingState;
        [SerializeField]
        GameObject UIPlayingState;

        //ValueFields
        [SerializeField]
        Text Money;
        [SerializeField]
        Text EnemiesLeft;
        [SerializeField]
        Text TowerRange, TowerDMG, TowerFireRate, TowerType;
        //Other
        GameState CurrentState;
        //Startup
        void Awake()
        {
            UIMainBody.SetActive(false);
            GameManager.instance.onLoadLevel += Instance_onLoadLevel;
            GameManager.instance.onStateChange += SetState;
        }

        private void Instance_onLoadLevel()
        {
            UIMainBody.SetActive(true);
            SetState(GameState.building);
        }

        public void StartWave()
        {
            GameManager.instance.StartWave();
        }

        public void SetState(GameState newState)
        {
            switch (newState)
            {
                case GameState.building:
                    UIBuildingState.SetActive(true);
                    UIPlayingState.SetActive(false);
                    break;
                case GameState.editing:
                    UIBuildingState.SetActive(false);
                    UIPlayingState.SetActive(false);
                    UIEditingState.SetActive(true);
                    break;

                case GameState.playing:
                    UIBuildingState.SetActive(false);
                    UIPlayingState.SetActive(true);                   
                    break;
            }
            CurrentState = newState;
        }

        void Update()
        {
            switch (CurrentState)
            {
                case GameState.building:
                    BuildingUpdate();
                    break;
                case GameState.editing:
                    EditUpdate();
                    break;
                case GameState.playing:
                    GameUpdate();
                    break;
            }
            SharedUpdate();
        }

        void GameUpdate()
        {
            EnemiesLeft.text = WaveManager.instance.EnemiesLeft.ToString();
        }

        void SharedUpdate()
        {
            Money.text = ResourceManager.cash.ToString();

            if (ContextMenus.TowerContextMenu.instance.currentTower !=null)
            {
                BaseTower t = ContextMenus.TowerContextMenu.instance.currentTower;
                TowerDMG.text = (Mathf.Round(t.damage * 100f) / 100f).ToString();
                TowerFireRate.text = (Mathf.Round(t.fireRate * 100f) / 100f).ToString();
                TowerRange.text = (Mathf.Round(t.range * 100f) / 100f).ToString();
                TowerType.text = t.type;
            }
            else
            {
                TowerDMG.text = "";
                TowerFireRate.text = "";
                TowerRange.text = "";
                TowerType.text = "";
            }
        }

        void BuildingUpdate()
        {

        }

        void EditUpdate()
        {

        }
    }
}