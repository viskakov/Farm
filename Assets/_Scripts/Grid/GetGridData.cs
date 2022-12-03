using UnityEngine;
using UnityEngine.EventSystems;

namespace Farm._Scripts
{
    public sealed class GetGridData : MonoBehaviour
    {
        [SerializeField] private LayerMask _gridLayerMask;

        private Camera _mainCamera;

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
                Debug.Log(cell.name);
            }
        }
    }
}