using Farm.Grid;
using Farm.Helpers;
using Farm.Player;

namespace Farm.Commands
{
    public sealed class CutDownCommand : ICommand
    {
        private readonly CellLogic _cell;

        public CutDownCommand(CellLogic cell)
        {
            _cell = cell;
        }

        public void Execute()
        {
            void HarvestAction() => _cell.Harvest();

            // TODO new state
            var nextState = PlayerController.Instance.PickupState;

            // TODO new animation hash
            var newTask = new Task(_cell.transform.position, nextState, AnimatorHash.Pickup, HarvestAction);
            PlayerController.Instance.SetTask(newTask);
        }
    }
}