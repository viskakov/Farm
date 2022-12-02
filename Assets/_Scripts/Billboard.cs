using UnityEngine;

namespace Farm._Scripts
{
    public class Billboard : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.forward = _mainCamera.transform.forward;
        }
    }
}