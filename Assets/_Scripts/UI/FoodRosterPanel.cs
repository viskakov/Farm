using System.Collections.Generic;
using System.Linq;
using Farm._Scripts.Items;
using UnityEngine;

namespace Farm._Scripts
{
    public sealed class FoodRosterPanel : MonoBehaviour
    {
        [SerializeField] private ButtonView _buttonPrefab;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _parent;

        private List<FoodData> _foods;

        public CellLogic CurrentCell { get; private set; }

        private void Awake()
        {
            LoadFoodData();
            CreateButtons();
        }

        private void LoadFoodData()
        {
            _foods = new List<FoodData>();
            _foods = Resources.LoadAll<FoodData>("Foods").ToList();
        }

        private void Start()
        {
            CellSelector.OnCellClicked += OnCellClickedHandler;
            Hide();
        }

        private void OnDestroy()
        {
            CellSelector.OnCellClicked -= OnCellClickedHandler;
        }

        private void OnCellClickedHandler(CellLogic cell)
        {
            if (cell)
            {
                CurrentCell = cell;
                Show();
            }
            else
            {
                CurrentCell = null;
                Hide();
            }
        }

        private void CreateButtons()
        {
            var count = _foods.Count;
            for (var i = 0; i < count; i++)
            {
                var instance = Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity, _parent);
                instance.name = $"{_foods[i].Name} button";
                instance.Init(this, _foods[i]);
            }
        }

        private void Show()
        {
            _canvasGroup.alpha = 1f;
        }

        private void Hide()
        {
            _canvasGroup.alpha = 0f;
        }
    }
}