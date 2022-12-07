using Farm.Food;
using Farm.FSM;
using Farm.FSM.States.CellStates;
using UnityEngine;

namespace Farm.Grid
{
    public sealed class CellLogic : MonoBehaviour
    {
        private IState _freeState;
        private IState _plantedState;
        private StateMachine _stateMachine;
        private MeshRenderer _meshRenderer;

        private bool IsFree => _stateMachine.CurrentState == _freeState;

        private void Awake()
        {
            _freeState = new FreeState();
            _plantedState = new PlantedState();
            _stateMachine = new StateMachine(_freeState);
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            CellSelector.OnCellClicked += OnCellClickedHandler;
        }

        private void OnDestroy()
        {
            CellSelector.OnCellClicked -= OnCellClickedHandler;
        }

        private void OnCellClickedHandler(CellLogic cell)
        {
            if (!cell || cell != this)
            {
                Unselect();
                return;
            }

            Select();
        }

        private void ChangeState(IState state)
        {
            _stateMachine.ChangeState(state);
        }

        public void Plant(FoodLogic foodLogic)
        {
            if (!IsFree)
            {
                return;
            }

            Instantiate(foodLogic, transform.position, Quaternion.identity, transform);
            ChangeState(_plantedState);
        }

        private void Select()
        {
            _meshRenderer.material.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        }

        private void Unselect()
        {
            _meshRenderer.material.color = Color.white;
        }
    }
}