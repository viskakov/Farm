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

            CellLogic cell = null;
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitData,Mathf.Infinity, _cellLayerMask))
            {
                cell = hitData.transform.GetComponent<CellLogic>();
            }

            OnCellClicked?.Invoke(cell);
        }
    }
}