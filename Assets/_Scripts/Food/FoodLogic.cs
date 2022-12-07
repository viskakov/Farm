using Farm._Scripts.Items;
using Farm.States;
using UnityEngine;

namespace Farm._Scripts
{
    public sealed class FoodLogic : MonoBehaviour
    {
        [SerializeField] private FoodData _foodData;
        [SerializeField] private GameObject _foodRender;
        [SerializeField] private GrowTimerUI _growTimerUI;

        private IState _ripeState;
        private IState _growState;
        private StateMachine _stateMachine;

        public IState RipeState => _ripeState;

        private void Awake()
        {
            _ripeState = new RipeState(_foodData, _foodRender);
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