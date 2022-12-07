namespace Farm.FSM
{
    public sealed class StateMachine
    {
        public IState CurrentState { get; private set; }

        public StateMachine(IState state)
        {
            CurrentState = state;
            CurrentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            if (CurrentState == newState)
            {
                return;
            }

            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
