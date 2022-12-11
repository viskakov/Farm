using Farm.Grid;
using Farm.Player;
using UnityEngine;

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
            Debug.Log("TO DO cut down command");
            void HarvestAction() => _cell.Harvest();
            // PlayerMovement.Instance.SetDestination(_cell.transform.position, HarvestAction);
        }
    }
}