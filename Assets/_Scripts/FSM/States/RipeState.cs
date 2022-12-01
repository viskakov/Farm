using Farm._Scripts;
using UnityEngine;

namespace Farm.States
{
    public sealed class RipeState : IState
    {
        private readonly Context _context;

        public RipeState(Context context)
        {
            _context = context;
        }

        public void Enter()
        {
            Debug.Log($"Enter {GetType().Name}");
            _context.transform.localScale = Vector3.one;
        }

        public void Update()
        {
            Debug.Log($"Update {GetType().Name}");
        }

        public void Exit()
        {
            Debug.Log($"Exit {GetType().Name}");
        }
    }
}