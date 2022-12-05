using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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

        public CellLogic SelectedCell { get; private set; }

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
            _canvasGroup.alpha = 0f;
        }

        private void OnDestroy()
        {
            CellSelector.OnCellClicked -= OnCellClickedHandler;
        }

        private void OnCellClickedHandler(CellLogic cell)
        {
            if (cell)
            {
                SelectedCell = cell;
                Show();
            }
            else
            {
                SelectedCell = null;
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
            void ScaleIn()
            {
                transform.localScale = Vector3.zero;
                transform
                    .DOScale(Vector3.one, 0.3f)
                    .SetEase(Ease.OutBack);
            }

            void FadeIn()
            {
                _canvasGroup
                    .DOFade(1f, 0.3f)
                    .SetEase(Ease.OutQuad);
            }

            ScaleIn();
            FadeIn();
        }

        public void Hide()
        {
            void ScaleOut()
            {
                transform
                    .DOScale(Vector3.zero, 0.2f)
                    .SetEase(Ease.OutQuad);
            }

            void FadeOut()
            {
                _canvasGroup
                    .DOFade(0f, 0.2f)
                    .SetEase(Ease.OutQuad);
            }

            ScaleOut();
            FadeOut();
        }
    }
}