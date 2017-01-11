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

        public int cash = 0;
        public int cashGainedWave;
        public int cashGainedLevel;

        public int Health;
        void Start()
        {
            GameManager.instance.onLoadLevel += onLoadLevel;
        }

        void onLoadLevel()
        {
            cash = GameManager.currentLevel.resources.startCash;
        }

        public void addCash(int value)
        {
            if (value < 0)
            {
                Debug.Log("USE BUY Function instead!");
                return;
            }
            cash += value;
            cashGainedWave += value;
            cashGainedLevel += value;
        }

        public static bool canBuy(int value)
        {
            return (instance.cash >= value);
        }

        public static void Buy(int value)
        {
            if (canBuy(value))
            {
                instance.cash -= value;
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