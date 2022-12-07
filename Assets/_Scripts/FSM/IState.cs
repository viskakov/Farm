namespace Farm.FSM
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}
