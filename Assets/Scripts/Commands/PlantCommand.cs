using Farm.Grid;
using Farm.Helpers;
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
            var nextState = PlayerController.Instance.PlantState;
            var newTask = new Task(_cell.transform.position, nextState, AnimatorHash.Plant, PlantAction);
            PlayerController.Instance.SetTask(newTask);
        }
    }
}