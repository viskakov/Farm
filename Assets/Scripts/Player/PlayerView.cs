using UnityEngine;

namespace Farm.Player
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private GameObject _handWateringCan;

        public GameObject HandWateringCan => _handWateringCan;

        private void Start()
        {
            _handWateringCan.SetActive(false);
        }
    }
}