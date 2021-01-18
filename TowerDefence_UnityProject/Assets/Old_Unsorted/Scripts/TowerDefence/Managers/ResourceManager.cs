using UnityEngine;
using System.Collections;

namespace TowerDefence.Managers
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager instance
        {
            get
            {
                if (_instance)
                    return _instance;
                _instance = FindObjectOfType<ResourceManager>();
                if (_instance)
                    return _instance;

                Debug.LogError("NO ResourceManager FOUND! Check what is calling it");
                return null;
            }
        }
        static ResourceManager _instance;

        public static int cash { get { return instance._cash; } }

        public int _cash;
        public int cashGainedWave;
        public int cashGainedLevel;

        public int Health;
        void Start()
        {
            GameManager.instance.onLoadLevel += onLoadLevel;
            ObjectPools.EnemyPool.instance.onRemove += Instance_onRemove;
        }

        private void Instance_onRemove(Enemies.BaseEnemy item)
        {
            addCash(item.resourceDropQuantity);
        }

        void onLoadLevel()
        {
            _cash = GameManager.currentLevel.resources.startCash;
        }

        public void addCash(int value)
        {
            //Debug.Log("Cash added " + value);
            if (value < 0)
            {
                Debug.Log("USE BUY Function instead!");
                return;
            }
            _cash += value;
            cashGainedWave += value;
            cashGainedLevel += value;
        }

        public static bool canBuy(int value)
        {
            return (instance._cash >= value);
        }

        public static void Buy(int value)
        {
            if (canBuy(value))
            {
                instance._cash -= value;
            }
        }

        public static void RemoveHealth(int value)
        {
            instance.Health -= value;
            if(instance.Health<= 0)
            {
                //TODO: trigger gameover State
            }
        }


    }
}