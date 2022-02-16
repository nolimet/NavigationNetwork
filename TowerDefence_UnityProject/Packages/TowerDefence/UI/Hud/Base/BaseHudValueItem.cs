using TMPro;
using UnityEngine;

namespace TowerDefence.UI.Hud
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