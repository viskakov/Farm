using DG.Tweening;
using UnityEngine;

namespace Farm.States
{
    public sealed class RipeState : IState
    {
        private readonly GameObject _foodRender;

        public RipeState(GameObject foodRender)
        {
            _foodRender = foodRender;
        }

        public void Enter()
        {
            _foodRender.transform
                .DOPunchScale(Vector3.one * 0.1f, 0.3f, 3, 0.3f);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}