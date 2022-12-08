using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Farm.Grid
{
    public sealed class CellSelector : MonoBehaviour
    {
        [SerializeField] private LayerMask _cellLayerMask;

        private Camera _mainCamera;

        public static Action<CellLogic> OnCellClicked;
        public static Action<CellLogic> OnCellHighlight;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            CellLogic cell = null;
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitData,Mathf.Infinity, _cellLayerMask))
            {
                cell = hitData.transform.GetComponent<CellLogic>();
                OnCellHighlight?.Invoke(cell);
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnCellClicked?.Invoke(cell);
            }
        }
    }
}