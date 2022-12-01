using UnityEngine;

namespace Farm._Scripts
{
    public class GetGridData : MonoBehaviour
    {
        [SerializeField] private LayerMask _gridLayerMask;
        [SerializeField] private GameObject[] _gameObjects;

        private GameObject _selectedCell;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hitData,Mathf.Infinity, _gridLayerMask))
                {
                    var cell = hitData.collider.gameObject;
                    DeselectCell();
                    SelectCell(cell);

                    var randomIndex = Random.Range(0, _gameObjects.Length);
                    Instantiate(_gameObjects[randomIndex], hitData.transform.position, Quaternion.identity,
                        hitData.transform);
                }
                else
                {
                    DeselectCell();
                }
            }
        }

        private void SelectCell(GameObject cell)
        {
            cell.GetComponent<MeshRenderer>().material.color = Color.green;
            _selectedCell = cell;
        }

        private void DeselectCell()
        {
            if (_selectedCell == null)
            {
                return;
            }

            _selectedCell.GetComponent<MeshRenderer>().material.color = Color.white;
            _selectedCell = null;
        }
    }
}