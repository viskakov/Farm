namespace Farm
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}
