using System;
using Farm.FSM;
using Farm.FSM.States.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Farm.Player
{
    public sealed class Task
    {
        public Vector3 Position { get; }
        public IState NextState { get; }
        public int AnimationHash { get; }
        public Action Action { get; }

        public Task(Vector3 position, IState nextState, int animationHash, Action action)
        {
            Position = position;
            NextState = nextState;
            AnimationHash = animationHash;
            Action = action;
        }
    }

    public sealed class PlayerMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private PlayerAnimator _playerAnimator;
        private PlayerView _playerView;
        private StateMachine _stateMachine;

        public IState IdleState { get; private set; }
        public IState WalkState { get; private set; }
        public IState PlantState { get; private set; }
        public IState PickupState { get; private set; }
        public Task Task { get; private set; }

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
            _playerView = GetComponent<PlayerView>();

            IdleState = new IdleState();
            WalkState = new WalkState(this, _playerAnimator, _agent);
            PlantState = new PlantState(this, _playerAnimator, _playerView);
            PickupState = new PickupState(this, _playerAnimator);

            _stateMachine = new StateMachine(IdleState);
        }

        public void ChangeState(IState state)
        {
            _stateMachine.ChangeState(state);
        }

        public void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        public void SetTask(Task task)
        {
            Task = task;
            ChangeState(WalkState);
        }
    }
}