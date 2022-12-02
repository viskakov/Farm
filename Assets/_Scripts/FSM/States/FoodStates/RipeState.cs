using DG.Tweening;
using UnityEngine;

namespace Farm.States
{
    public sealed class RipeState : IState
    {
        private readonly GameObject _foodModel;

        public RipeState(GameObject foodModel)
        {
            _foodModel = foodModel;
        }

        public void Enter()
        {
            _foodModel.transform
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