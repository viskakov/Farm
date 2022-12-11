using Farm.Helpers;
using Farm.Player;

namespace Farm.FSM.States.Player
{
    public sealed class PickupState : IState
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerAnimator _playerAnimator;
        private int _actionAnimationHash;

        public PickupState(PlayerMovement playerMovement, PlayerAnimator playerAnimator)
        {
            _playerMovement = playerMovement;
            _playerAnimator = playerAnimator;
        }

        public void Enter()
        {
            _actionAnimationHash = _playerMovement.Task.AnimationHash;
            _playerAnimator.TriggerAnimation(_actionAnimationHash);
        }

        public void Update()
        {
            var stateInfo = _playerAnimator.Animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(nameof(AnimatorHash.Pickup)) && stateInfo.normalizedTime >= 0.9f)
            {
                Pickup();
            }
        }

        public void Exit()
        {
        }

        private void Pickup()
        {
            _playerMovement.Task.Action?.Invoke();
            _playerMovement.ChangeState(_playerMovement.IdleState);
        }
    }
}