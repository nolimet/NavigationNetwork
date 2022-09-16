using TMPro;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Game.Hud.Drawers.TowerPlace
{
    internal sealed class TowerPlaceButton : MonoBehaviour
    {
        public delegate void CallbackDelegate(string towerId);
        private CallbackDelegate callback;
        private string towerId;
        [SerializeField] private TextMeshProUGUI label;

        public void OnClicked()
        {
            callback(towerId);
        }

        public class Factory : PlaceholderFactory<TowerPlaceButton>
        {
            public TowerPlaceButton Create(Transform container, string labelText, string towerId, CallbackDelegate onClick)
            {
                var towerPlaceButton = base.Create();
                towerPlaceButton.transform.SetParent(container);

                towerPlaceButton.label.text = labelText;
                towerPlaceButton.callback = onClick;
                towerPlaceButton.towerId = towerId;

                return towerPlaceButton;
            }
        }
    }
}
