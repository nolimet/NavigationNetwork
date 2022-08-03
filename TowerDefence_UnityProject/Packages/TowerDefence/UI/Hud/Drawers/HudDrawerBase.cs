using TowerDefence.Systems.Selection;
using UnityEngine;

namespace TowerDefence.UI.Hud
{
    internal abstract class HudDrawerBase : MonoBehaviour
    {
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public abstract bool DrawsType(ISelectable selectable);

        public abstract void SetValue(ISelectable selectable);
    }

    internal abstract class HudDrawerBase<T> : HudDrawerBase where T : class, ISelectable
    {
        public override bool DrawsType(ISelectable selectable) => selectable is T;
    }
}
