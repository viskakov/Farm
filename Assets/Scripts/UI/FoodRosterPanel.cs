using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private WaitForSeconds _pause;

        public CellLogic SelectedCell { get; private set; }

        private void Awake()
        {
            _items = new List<ButtonView>();
            _pause = new WaitForSeconds(0.1f);

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
                switch (cell.CurrentFood.FoodKind)
                {
                    case FoodKind.Carrot:
                    {
                        var pickupCommand = new PickupCommand(cell);
                        pickupCommand.Execute();
                        break;
                    }
                    case FoodKind.Grass:
                    {
                        var cutDownCommand = new CutDownCommand(cell);
                        cutDownCommand.Execute();
                        break;
                    }
                    case FoodKind.Tree:
                    {
                        var doNothingCommand = new DoNothingCommand();
                        doNothingCommand.Execute();
                        break;
                    }
                    default:
                    {
                        Debug.Log("Undefined");
                        throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private void CreateFoodButtons()
        {
            for (var i = 0; i < _foods.Count; i++)
            {
                var instance = Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity, _parent);
                instance.name = $"{_foods[i].Name} button";
                instance.Init(this, _foods[i]);
                _items.Add(instance);
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
            StartCoroutine(ShowItemEffect());
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

        private IEnumerator ShowItemEffect()
        {
            foreach (var item in _items)
            {
                item.transform.localScale = Vector3.zero;
            }

            foreach (var item in _items)
            {
                yield return _pause;

                item.transform
                    .DOScale(Vector3.one, 0.3f)
                    .SetEase(Ease.OutBack);
            }
        }
    }
}