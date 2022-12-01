using UnityEngine;

namespace Farm._Scripts
{
    public class GridSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _gridPrefab;
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        private void Awake()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            var parent = new GameObject("Grid").transform;
            var counter = 0;
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    var instance = Instantiate(_gridPrefab, new Vector3(i, 0f, j), _gridPrefab.transform.rotation, parent);
                    instance.name = $"Grid {counter}";
                    counter++;
                }
            }
        }
    }
}