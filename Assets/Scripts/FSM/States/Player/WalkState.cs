using Farm.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Farm.FSM.States.Player
{
    public sealed class WalkState : IState
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerAnimator _playerAnimator;
        private readonly NavMeshAgent _agent;

        public WalkState(PlayerMovement playerMovement, PlayerAnimator playerAnimator, NavMeshAgent agent)
        {
            _playerMovement = playerMovement;
            _playerAnimator = playerAnimator;
            _agent = agent;
        }

        public void Enter()
        {
            MoveTo();
        }

        public void Update()
        {
            UpdateAnimator();

            if (_agent.pathPending || _agent.remainingDistance > Mathf.Epsilon)
            {
                return;
            }

            _playerMovement.ChangeState(_playerMovement.PlantState);
        }

        public void Exit()
        {
        }

        private void UpdateAnimator()
        {
            var isMoving = _agent.velocity.magnitude > 0f;
            _playerAnimator.PlayWalkAnimation(isMoving);
        }

        private void MoveTo()
        {
            _agent.SetDestination(_playerMovement.Destination);
        }
    }
}