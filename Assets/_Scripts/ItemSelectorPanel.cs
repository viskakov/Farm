using System.Collections.Generic;
using System.Linq;
using Farm._Scripts.Items;
using UnityEngine;

namespace Farm._Scripts
{
    public class ItemSelectorPanel : MonoBehaviour
    {
        [SerializeField] private List<FoodData> _foods;
        [SerializeField] private ButtonView _buttonPrefab;
        [SerializeField] private Transform _parent;

        private void Awake()
        {
            LoadFoodData();
        }

        private void Start()
        {
            CreateButtons();
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
                instance.Init(_foods[i]);
            }
        }
    }
}