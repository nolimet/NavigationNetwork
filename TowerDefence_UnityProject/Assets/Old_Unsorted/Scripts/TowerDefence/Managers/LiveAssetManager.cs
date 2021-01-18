using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefence.Managers
{
    public class LiveAssetManager : MonoBehaviour
    {
        public static LiveAssetManager instance
        {
            get
            {
                if (_instance)
                    return _instance;
                _instance = FindObjectOfType<LiveAssetManager>();
                if (_instance)
                    return _instance;

                Debug.LogError("NO LiveAssetManager FOUND! Check what is calling it");
                return null;
            }
        }
        static LiveAssetManager _instance;

        public Material laserMaterial;
    }
}