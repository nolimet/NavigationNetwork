﻿using NoUtil;
using UnityEngine;

namespace TowerDefence.UI
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField, ReadOnly] private Transform worldUIContainer;

        public Transform WorldUIContainer => worldUIContainer;

        [SerializeField, ReadOnly] private Transform screenUIContainer;

        public Transform ScreenUIContainer => screenUIContainer;

        public void Setup(Transform worldUIContainer, Transform screenUIContainer)
        {
            this.worldUIContainer = worldUIContainer;
            this.screenUIContainer = screenUIContainer;
        }

        public void ClearContainers()
        {
            foreach (Transform child in worldUIContainer)
            {
                Debug.Log(child.name);
            }

            foreach (Transform child in screenUIContainer)
            {
                Debug.Log(child.name);
            }
        }
    }
}