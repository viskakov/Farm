using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using Farm.Commands;
using Farm.Food;
using Farm.Grid;
using Food;
using UnityEngine;

namespace Farm.UI
{
    public sealed class FoodRosterPanel : MonoBehaviour
    {
        [SerializeField] private ButtonView _buttonPrefab;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _parent;

        private List<FoodData> _foods;
        private List<ButtonView> _items;
        private readonly float _pause = 0.1f;
        private readonly float _scaleInDuration = 0.3f;
        private readonly float _scaleOutDuration = 0.2f;
        private readonly float _fadeInDuration = 0.3f;
        private readonly float _fadeOutDuration = 0.2f;

        public CellLogic SelectedCell { get; private set; }

        private void Awake()
        {
            _items = new List<ButtonView>();

            LoadFoodData();
            CreateFoodButtons();
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

        // TODO Refactor this
        private void OnCellClickedHandler(CellLogic cell)
        {
            if (cell == null)
            {
                SelectedCell = null;
                Hide();
            }

            // Prevent spam panel show on same cell
            if (cell == SelectedCell)
            {
                return;
            }

            if (cell.IsFree)
            {
                SelectedCell = cell;
                Show();
                return;
            }

            SelectedCell = cell;
            Hide();

            if (cell.CurrentFood.IsRipe)
            {
                ICommand command = cell.CurrentFood.FoodKind switch
                {
                    FoodKind.Carrot => new PickupCommand(cell),
                    FoodKind.Grass => new CutDownCommand(cell),
                    FoodKind.Tree => new DoNothingCommand(),
                    _ => throw new ArgumentOutOfRangeException(),
                };
                command.Execute();
            }
        }

        private void CreateFoodButtons()
        {
            _items = _foods.Select(food =>
            {
                var instance = Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity, _parent);
                instance.name = $"{food.Name} button";
                instance.Init(this, food);
                return instance;
            }).ToList();
        }

        private async void Show()
        {
            void ScaleIn()
            {
                transform.localScale = Vector3.zero;
                transform
                    .DOScale(Vector3.one, _scaleInDuration)
                    .SetEase(Ease.OutBack);
            }

            void FadeIn()
            {
                _canvasGroup
                    .DOFade(1f, _fadeInDuration)
                    .SetEase(Ease.OutQuad);
            }

            ScaleIn();
            FadeIn();
            await ShowItemEffect();
        }

        public void Hide()
        {
            void ScaleOut()
            {
                transform
                    .DOScale(Vector3.zero, _scaleOutDuration)
                    .SetEase(Ease.OutQuad);
            }

            void FadeOut()
            {
                _canvasGroup
                    .DOFade(0f, _fadeOutDuration)
                    .SetEase(Ease.OutQuad);
            }

            ScaleOut();
            FadeOut();
        }

        private async Task ShowItemEffect()
        {
            foreach (var item in _items)
            {
                item.transform.localScale = Vector3.zero;
            }

            foreach (var item in _items)
            {
                await Task.Delay(TimeSpan.FromSeconds(_pause));

                item.transform
                    .DOScale(Vector3.one, _scaleInDuration)
                    .SetEase(Ease.OutBack);
            }
        }
    }
}