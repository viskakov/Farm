using Farm.Helpers;
using Farm.Player;

namespace Farm.FSM.States.Player
{
    public sealed class PlantState : IState
    {
        private readonly PlayerController _playerController;
        private readonly PlayerAnimator _playerAnimator;
        private readonly PlayerView _playerView;

        public PlantState(PlayerController playerController, PlayerAnimator playerAnimator, PlayerView playerView)
        {
            _playerController = playerController;
            _playerAnimator = playerAnimator;
            _playerView = playerView;
        }

        public void Enter()
        {
            _playerAnimator.TriggerAnimation(_playerController.Task.AnimationHash);
            _playerView.HandWateringCan.SetActive(true);
        }

        public void Update()
        {
            var stateInfo = _playerAnimator.Animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(nameof(AnimatorHash.Plant)) && stateInfo.normalizedTime >= 1f)
            {
                Plant();
            }
        }

        public void Exit()
        {
            _playerView.HandWateringCan.SetActive(false);
        }

        private void Plant()
        {
            _playerController.Task.Action?.Invoke();
            _playerController.ChangeState(_playerController.IdleState);
        }
    }
}