using Farm.Helpers;
using Farm.Player;

namespace Farm.FSM.States.Player
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
            _playerMovement.HandWateringCan.SetActive(true);
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
            _playerMovement.HandWateringCan.SetActive(false);
        }

        private void Plant()
        {
            _playerMovement.OnCompleted?.Invoke();
            _playerMovement.ChangeState(_playerMovement.IdleState);
        }
    }
}