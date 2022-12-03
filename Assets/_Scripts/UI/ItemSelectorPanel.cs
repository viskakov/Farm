using System.Collections.Generic;
using System.Linq;
using Farm._Scripts.Items;
using UnityEngine;

namespace Farm._Scripts
{
    public class ItemSelectorPanel : MonoBehaviour
    {
        [SerializeField] private ButtonView _buttonPrefab;
        [SerializeField] private Transform _parent;

        private List<FoodData> _foods;

        private void Awake()
        {
            LoadFoodData();
            CreateButtons();
            Hide();
        }

        private void LoadFoodData()
        {
            _foods = new List<FoodData>();
            _foods = Resources.LoadAll<FoodData>("Foods").ToList();
        }

        private void CreateButtons()
        {
            var count = _foods.Count;
            for (var i = 0; i < count; i++)
            {
                var instance = Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity, _parent);
                instance.name = $"{_foods[i].Name} button";
                instance.Init(_foods[i]);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}