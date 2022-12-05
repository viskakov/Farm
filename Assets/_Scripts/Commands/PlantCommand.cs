namespace Farm._Scripts.Commands
{
    public class PlantCommand : ICommand
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
            _cell.Plant(_food);
        }
    }
}