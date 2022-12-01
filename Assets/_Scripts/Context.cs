using Farm._Scripts.Items;
using Farm.States;
using UnityEngine;

namespace Farm._Scripts
{
    public class Context : MonoBehaviour
    {
        [SerializeField] private ItemToGrowData _itemToGrowData;

        public ItemToGrowData ItemToGrowData => _itemToGrowData;
        public IState RipeState;
        public IState GrowState;

        private StateMachine _stateMachine;

        private void Awake()
        {
            RipeState = new RipeState(this);
            GrowState = new GrowState(this);
            _stateMachine = new StateMachine(GrowState);
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