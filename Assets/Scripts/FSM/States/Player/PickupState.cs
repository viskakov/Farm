using Farm.Helpers;
using Farm.Player;

namespace Farm.FSM.States.Player
{
    public sealed class PickupState : IState
    {
        private readonly PlayerController _playerController;
        private readonly PlayerAnimator _playerAnimator;
        private int _actionAnimationHash;

        public PickupState(PlayerController playerController, PlayerAnimator playerAnimator)
        {
            _playerController = playerController;
            _playerAnimator = playerAnimator;
        }

        public void Enter()
        {
            _actionAnimationHash = _playerController.Task.AnimationHash;
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
            _playerController.Task.Action?.Invoke();
            _playerController.ChangeState(_playerController.IdleState);
        }
    }
}