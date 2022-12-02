using DG.Tweening;
using Farm._Scripts;
using UnityEngine;

namespace Farm.States
{
    public sealed class RipeState : IState
    {
        private readonly FoodLogic _foodLogic;

        public RipeState(FoodLogic foodLogic)
        {
            _foodLogic = foodLogic;
        }

        public void Enter()
        {
            _foodLogic.FoodModel.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 3, 0.3f);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}