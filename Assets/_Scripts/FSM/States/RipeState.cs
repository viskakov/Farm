using DG.Tweening;
using Farm._Scripts;
using UnityEngine;

namespace Farm.States
{
    public sealed class RipeState : IState
    {
        private readonly Food _food;

        public RipeState(Food food)
        {
            _food = food;
        }

        public void Enter()
        {
            _food.FoodObject.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 3, 0.3f);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}