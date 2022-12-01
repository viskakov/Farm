using UnityEngine;

namespace Farm.States
{
    public class FreeState : IState
    {
        public void Enter()
        {
            Debug.Log($"Enter {GetType().Name}");
        }

        public void Update()
        {
        }

        public void Exit()
        {
            Debug.Log($"Exit {GetType().Name}");
        }
    }
}