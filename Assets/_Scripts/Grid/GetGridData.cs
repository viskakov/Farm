using UnityEngine;
using UnityEngine.EventSystems;

namespace Farm._Scripts
{
    public sealed class GetGridData : MonoBehaviour
    {
        [SerializeField] private LayerMask _gridLayerMask;
        [SerializeField] private GameObject _foodSelector;

        private Camera _mainCamera;
        private CellLogic _selectedCell;

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
                var cell = hitData.transform.GetComponent<CellLogic>();
                if (cell)
                {
                    _selectedCell = cell;
                    _foodSelector.SetActive(true);
                }
                else
                {
                    _selectedCell = null;
                    _foodSelector.SetActive(false);
                }
            }
        }

        public void Test1()
        {
            Debug.Log(_selectedCell.name);
        }
        
        public void Test2()
        {
            Debug.Log("Test2");
        }
        
        public void Test3()
        {
            Debug.Log("Test3");
        }
    }
}