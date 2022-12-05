using System;
using Farm._Scripts.Commands;
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
            var timeSpan = TimeSpan.FromSeconds(_foodData.GrowTime);
            _duration.SetText($"Time {timeSpan:m':'ss}");
            _icon.sprite = foodData.Icon;
        }

        public void OnButtonClicked()
        {
            var plantCommand = new PlantCommand(_foodRosterPanel.SelectedCell, _foodData.FoodPrefab);
            plantCommand.Execute();

            _foodRosterPanel.Hide();
        }
    }
}