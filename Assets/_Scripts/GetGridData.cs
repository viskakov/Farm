using UnityEngine;

namespace Farm._Scripts
{
    public sealed class GetGridData : MonoBehaviour
    {
        [SerializeField] private LayerMask _gridLayerMask;
        [SerializeField] private Food[] _foods;

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

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitData,Mathf.Infinity, _gridLayerMask))
            {
                var cell = hitData.transform.GetComponent<Cell>();
                if (cell & cell.IsFree)
                {
                    var randomIndex = Random.Range(0, _foods.Length);
                    cell.Plant(_foods[randomIndex]);
                }
            }
        }
    }
}