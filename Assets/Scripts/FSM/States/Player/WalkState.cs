using Farm.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Farm.FSM.States.Player
{
    public sealed class WalkState : IState
    {
        private readonly PlayerController _playerController;
        private readonly PlayerAnimator _playerAnimator;
        private readonly NavMeshAgent _agent;

        public WalkState(PlayerController playerController, PlayerAnimator playerAnimator, NavMeshAgent agent)
        {
            _playerController = playerController;
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
            MoveTo();

            if (_agent.pathPending || _agent.remainingDistance > Mathf.Epsilon)
            {
                return;
            }

            _playerController.ChangeState(_playerController.Task.NextState);
        }

        public void Exit()
        {
        }

        private void UpdateAnimator()
        {
            var isMoving = _agent.velocity.magnitude > Mathf.Epsilon;
            _playerAnimator.PlayWalkAnimation(isMoving);
        }

        private void MoveTo()
        {
            _agent.SetDestination(_playerController.Task.Position);
        }
    }
}