using Farm.Grid;
using Farm.Helpers;
using Farm.Player;

namespace Farm.Commands
{
    public sealed class PickupCommand : ICommand
    {
        private readonly CellLogic _cell;

        public PickupCommand(CellLogic cell)
        {
            _cell = cell;
        }

        public void Execute()
        {
            void HarvestAction() => _cell.Harvest();
            var nextState = PlayerController.Instance.PickupState;
            var newTask = new Task(_cell.transform.position, nextState, AnimatorHash.Pickup, HarvestAction);
            PlayerController.Instance.SetTask(newTask);
        }
    }
}