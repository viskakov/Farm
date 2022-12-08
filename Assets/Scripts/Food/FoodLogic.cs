using Farm.FSM;
using Farm.FSM.States.FoodStates;
using Farm.UI;
using UnityEngine;

namespace Farm.Food
{
    public sealed class FoodLogic : MonoBehaviour
    {
        [SerializeField] private FoodData _foodData;
        [SerializeField] private GameObject _foodRender;
        [SerializeField] private GrowTimerUI _growTimerUI;
        [SerializeField] private ParticleSystem _particleSystem;

        private IState _ripeState;
        private IState _growState;
        private StateMachine _stateMachine;

        public IState RipeState => _ripeState;

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