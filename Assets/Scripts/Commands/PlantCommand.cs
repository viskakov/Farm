using Farm.Grid;
using Farm.Player;
using Food;

namespace Farm.Commands
{
    public sealed class PlantCommand : ICommand
    {
        private readonly CellLogic _cell;
        private readonly FoodBase _food;

        public PlantCommand(CellLogic cell, FoodBase food)
        {
            _cell = cell;
            _food = food;
        }

        public void Execute()
        {
            void PlantAction() => _cell.Plant(_food);
            PlayerMovement.Instance.SetDestination(_cell.transform.position, PlantAction);
        }
    }
}