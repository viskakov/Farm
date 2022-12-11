using Farm.Grid;
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
            PlayerMovement.Instance.SetDestination(_cell.transform.position, HarvestAction);
        }
    }
}