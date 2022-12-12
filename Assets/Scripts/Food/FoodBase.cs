using Farm.Food;
using Farm.FSM;
using Farm.FSM.States.FoodStates;
using Farm.UI;
using UnityEngine;

namespace Food
{
    public enum FoodKind
    {
        Carrot = 0,
        Grass = 1,
        Tree = 2
    }

    public abstract class FoodBase : MonoBehaviour
    {
        [SerializeField] private FoodData _foodData;
        [SerializeField] private GameObject _foodRender;
        [SerializeField] private GrowTimerUI _growTimerUI;
        [SerializeField] private ParticleSystem _particleSystem;

        private IState _ripeState;
        private IState _growState;
        private StateMachine _stateMachine;

        public bool IsRipe => _stateMachine.CurrentState == _ripeState;

        public IState RipeState => _ripeState;
        public FoodKind FoodKind { get; protected set; }

        public abstract bool Interact();

        private void Awake()
        {
            _ripeState = new RipeState(_foodData, _foodRender, _particleSystem);
            _growState = new GrowState(this, _foodData, _foodRender, _growTimerUI);
            _stateMachine = new StateMachine(_growState);
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        public void ChangeState(IState state)
        {
            _stateMachine.ChangeState(state);
        }
    }
}