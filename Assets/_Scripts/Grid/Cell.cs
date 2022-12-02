using Farm.States;
using UnityEngine;

namespace Farm._Scripts
{
    public sealed class Cell : MonoBehaviour
    {
        private IState _freeState;
        private IState _busyState;
        private StateMachine _stateMachine;

        public bool IsFree => _stateMachine.CurrentState == _freeState;

        private void Awake()
        {
            _freeState = new FreeState();
            _busyState = new BusyState();
            _stateMachine = new StateMachine(_freeState);
        }

        private void ChangeState(IState state)
        {
            _stateMachine.ChangeState(state);
        }

        public void Plant(FoodLogic foodLogic)
        {
            if (IsFree)
            {
                Instantiate(foodLogic, transform.position, Quaternion.identity, transform);
                ChangeState(_busyState);
            }
        }
    }
}