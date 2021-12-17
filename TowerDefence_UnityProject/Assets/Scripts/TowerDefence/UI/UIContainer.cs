using NoUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefence.UI
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Transform worldUIContainer;

        public Transform WorldUIContainer => worldUIContainer;

        [SerializeField, ReadOnly]
        private Transform screenUIContainer;

        public Transform ScreenUIContainer => screenUIContainer;

        public void Setup(Transform worldUIContainer, Transform screenUIContainer)
        {
            this.worldUIContainer = worldUIContainer;
            this.screenUIContainer = screenUIContainer;
        }
    }
}