using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Farm._Scripts
{
    public sealed class CellSelector : MonoBehaviour
    {
        [SerializeField] private LayerMask _cellLayerMask;

        private Camera _mainCamera;
        private CellLogic _cell;

        public static Action<CellLogic> OnCellClicked;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitData,Mathf.Infinity, _cellLayerMask))
            {
                var cell = hitData.transform.GetComponent<CellLogic>();
                if (cell == _cell)
                {
                    return;
                }

                if (_cell)
                {
                    _cell.Unselect();
                }

                _cell = cell;
                _cell.Select();
            }
            else
            {
                if (_cell)
                {
                    _cell.Unselect();
                    _cell = null;
                }
            }

            OnCellClicked?.Invoke(_cell);
        }
    }
}