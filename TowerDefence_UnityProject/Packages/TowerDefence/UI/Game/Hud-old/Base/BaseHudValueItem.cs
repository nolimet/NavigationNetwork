using TMPro;
using UnityEngine;

namespace TowerDefence.UI.Game.Hud_old.Base
{
    public class BaseHudValueItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI labelField, valueField;

        public void SetValue(string value)
        {
            valueField.text = value;
        }

        public void SetLabel(string value)
        {
            labelField.text = value;
        }
    }
}