using UnityEngine;

namespace Farm._Scripts
{
    public sealed class GetGridData : MonoBehaviour
    {
        [SerializeField] private LayerMask _gridLayerMask;
        [SerializeField] private Context[] _gameObjects;

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
                if (cell != null && cell.IsFree)
                {
                    var randomIndex = Random.Range(0, _gameObjects.Length);
                    cell.Plant(_gameObjects[randomIndex]);
                }
            }
        }
    }
}