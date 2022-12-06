namespace Farm._Scripts.Commands
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
            // TO DO
        }
    }
}