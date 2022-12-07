using Farm.Grid;

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
            // TO DO
        }
    }
}