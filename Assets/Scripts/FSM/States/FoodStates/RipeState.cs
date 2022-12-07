using DG.Tweening;
using Farm.Food;
using GameStat;
using UnityEngine;

namespace Farm.FSM.States.FoodStates
{
    public sealed class RipeState : IState
    {
        private readonly FoodData _foodData;
        private readonly GameObject _foodRender;

        public RipeState(FoodData foodData, GameObject foodRender)
        {
            _foodData = foodData;
            _foodRender = foodRender;
        }

        public void Enter()
        {
            _foodRender.transform
                .DOPunchScale(Vector3.one * 0.1f, 0.3f, 3, 0.3f);

            GameStatManager.AddExperience(_foodData.GetExperience());
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}