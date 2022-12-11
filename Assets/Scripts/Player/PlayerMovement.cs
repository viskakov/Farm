using System;
using Farm.FSM;
using Farm.FSM.States.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Farm.Player
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _handWateringCan;

        private NavMeshAgent _agent;
        private PlayerAnimator _playerAnimator;
        private StateMachine _stateMachine;
        private IState _walkState;

        public IState IdleState { get; private set; }
        public IState PlantState { get; private set; }
        public Vector3 Destination { get; private set; }
        public Action OnCompleted;
        public GameObject HandWateringCan => _handWateringCan;

        public static PlayerMovement Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _agent = GetComponent<NavMeshAgent>();
            _playerAnimator = GetComponent<PlayerAnimator>();

            _walkState = new WalkState(this, _playerAnimator, _agent);
            IdleState = new IdleState();
            PlantState = new PlantState(this, _playerAnimator);

            _stateMachine = new StateMachine(IdleState);
            HandWateringCan.SetActive(false);
        }

        public void ChangeState(IState state)
        {
            _stateMachine.ChangeState(state);
        }

        public void SetDestination(Vector3 destination, Action onCompleted)
        {
            if (_stateMachine.CurrentState == PlantState)
            {
                return;
            }

            Destination = destination;
            OnCompleted = onCompleted;

            ChangeState(_walkState);
        }

        public void Update()
        {
            _stateMachine.CurrentState.Update();
        }
    }
}