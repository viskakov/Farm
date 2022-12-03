using UnityEngine;
using UnityEngine.EventSystems;

namespace Farm._Scripts
{
    public sealed class GetGridData : MonoBehaviour
    {
        [SerializeField] private LayerMask _gridLayerMask;
        [SerializeField] private ItemSelectorPanel _itemSelectorPanel;

        private Camera _mainCamera;

        public static GetGridData Instance;
        public CellLogic SelectedCell { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

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
                SelectedCell = hitData.transform.GetComponent<CellLogic>();
                _itemSelectorPanel.Show();
            }
            else
            {
                SelectedCell = null;
                _itemSelectorPanel.Hide();
            }
        }
    }
}