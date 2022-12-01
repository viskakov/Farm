using DG.Tweening;
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
            _context.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 3, 0.3f);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}