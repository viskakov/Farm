using Farm._Scripts.Helpers;
using Farm._Scripts.Player;
using TreasureHunter;

namespace Farm.States.Player
{
    public sealed class PlantState : IState
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerAnimator _playerAnimator;

        public PlantState(PlayerMovement playerMovement, PlayerAnimator playerAnimator)
        {
            _playerMovement = playerMovement;
            _playerAnimator = playerAnimator;
        }

        public void Enter()
        {
            _playerAnimator.PlayPlantAnimation();
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
        }

        private void Plant()
        {
            _playerMovement.OnCompleted?.Invoke();
            _playerMovement.ChangeState(_playerMovement.IdleState);
        }
    }
}