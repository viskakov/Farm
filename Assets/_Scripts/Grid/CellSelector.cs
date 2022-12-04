using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Farm._Scripts
{
    public sealed class CellSelector : MonoBehaviour
    {
        [SerializeField] private LayerMask _gridLayerMask;

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
            if (Physics.Raycast(ray, out var hitData,Mathf.Infinity, _gridLayerMask))
            {
                if (_cell)
                {
                    _cell.Unselect();
                }

                _cell = hitData.transform.GetComponent<CellLogic>();
                _cell.Select();
                OnCellClicked?.Invoke(_cell);
            }
            else
            {
                _cell.Unselect();
                OnCellClicked?.Invoke(null);
            }
        }
    }
}