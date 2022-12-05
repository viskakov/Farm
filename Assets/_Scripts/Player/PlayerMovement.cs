using System;
using Farm._Scripts.Helpers;
using UnityEngine;
using UnityEngine.AI;

namespace TreasureHunter
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speedRotation = 5f;

        public static Action<RaycastHit> ClickedEvent;

        private NavMeshAgent _agent;
        private Camera _mainCamera;
        private Vector3 _moveTarget;
        private Vector3 _direction;
        private Quaternion _lookRotation;
        private bool _isNeedToRotate;
        private bool IsMoving => _agent.velocity.magnitude > Mathf.Epsilon;

        private void Awake()
        {
            _mainCamera = Camera.main;
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
                    _agent.SetDestination(_moveTarget);
                    _isNeedToRotate = false;
                }
            }

            if (Input.GetMouseButton(0))
            {
                ClickToMove();
            }
        }

        private void ClickToMove()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitData, 50f))
            {
                if (NavMesh.SamplePosition(hitData.point, out var navMeshHit, 0.25f, 1 << 0))
                {
                    ClickedEvent?.Invoke(hitData);

                    StopNavigation();

                    _moveTarget = navMeshHit.position;
                    _direction = (_moveTarget.WithNewY(transform.position.y) - transform.position).normalized;
                    _lookRotation = Quaternion.LookRotation(_direction, Vector3.up);
                    _isNeedToRotate = true;
                }
            }
        }

        private void StopNavigation()
        {
            _agent.SetDestination(transform.position);
        }
    }
}