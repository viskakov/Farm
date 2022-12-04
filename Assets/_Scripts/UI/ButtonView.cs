using Farm._Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farm._Scripts
{
    public sealed class ButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _duration;
        [SerializeField] private Image _icon;

        private FoodRosterPanel _foodRosterPanel;
        private FoodData _foodData;
        
        public void Init(FoodRosterPanel foodRosterPanel, FoodData foodData)
        {
            _foodRosterPanel = foodRosterPanel;
            _foodData = foodData;
            _name.SetText(foodData.Name);
            _duration.SetText($"{_foodData.GrowDuration} seconds");
            _icon.sprite = foodData.Icon;
        }

        public void OnButtonClicked()
        {
            if (_foodRosterPanel.CurrentCell)
            {
                _foodRosterPanel.CurrentCell.Plant(_foodData.FoodPrefab);
            }

            _foodRosterPanel.Hide();
        }
    }
}