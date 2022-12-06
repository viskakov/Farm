using TreasureHunter;

namespace Farm._Scripts.Commands
{
    public sealed class PlantCommand : ICommand
    {
        private readonly CellLogic _cell;
        private readonly FoodLogic _food;

        public PlantCommand(CellLogic cell, FoodLogic food)
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