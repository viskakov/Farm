using UnityEngine;

namespace Farm.Grid
{
    public sealed class GridSpawner : MonoBehaviour
    {
        [SerializeField] private CellLogic _cellLogicPrefab;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] [Range(1f, 2f)] private float _spacing;

        private void Awake()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            var counter = 0;
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    var instance = Instantiate(_cellLogicPrefab, transform.position + new Vector3(i * _spacing, 0f, j * _spacing), 
                        _cellLogicPrefab.transform.rotation, transform);
                    instance.name = $"Cell_{counter}";
                    counter++;
                }
            }
        }
    }
}