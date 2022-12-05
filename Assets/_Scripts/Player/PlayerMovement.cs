using System;
using System.Threading.Tasks;
using Farm._Scripts.Helpers;
using UnityEngine;
using UnityEngine.AI;

namespace TreasureHunter
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speedRotation = 5f;

        private NavMeshAgent _agent;
        private Vector3 _direction;
        private Quaternion _lookRotation;
        private bool _isNeedToRotate;

        public static PlayerMovement Instance;
        public static Action OnReachPosition;
        public bool IsMoving => _agent.velocity.magnitude > 0f;

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

            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_isNeedToRotate)
            {
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, _lookRotation, _speedRotation * Time.deltaTime);
            
                if (Vector3.Dot(_direction, transform.forward) >= 0.97f)
                {
                    _isNeedToRotate = false;
                }
            }
        }

        public async Task MoveToAsync(Vector3 position)
        {
            _direction = (position.WithNewY(transform.position.y) - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction, Vector3.up);
            _isNeedToRotate = true;

            _agent.SetDestination(position);
            while (_agent.pathPending || _agent.remainingDistance > Mathf.Epsilon)
            {
                await Task.Yield();
            }

            OnReachPosition?.Invoke();
        }
    }
}