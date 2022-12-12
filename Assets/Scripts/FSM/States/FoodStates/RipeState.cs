using DG.Tweening;
using Farm.Food;
using GameData;
using UnityEngine;

namespace Farm.FSM.States.FoodStates
{
    public sealed class RipeState : IState
    {
        private readonly FoodData _foodData;
        private readonly GameObject _foodRender;
        private readonly ParticleSystem _particleSystem;

        public RipeState(FoodData foodData, GameObject foodRender, ParticleSystem particleSystem)
        {
            _foodData = foodData;
            _foodRender = foodRender;
            _particleSystem = particleSystem;
        }

        public void Enter()
        {
            _foodRender.transform
                .DOPunchScale(Vector3.one * 0.1f, 0.3f, 3, 0.3f);

            _particleSystem.Play();

            GameDataManager.AddExperience(_foodData.GetExperience());
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}