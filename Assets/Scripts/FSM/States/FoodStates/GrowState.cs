using Farm.Food;
using Farm.UI;
using Food;
using UnityEngine;

namespace Farm.FSM.States.FoodStates
{
    public sealed class GrowState : IState
    {
        private readonly FoodBase _foodBase;
        private readonly FoodData _foodData;
        private readonly GameObject _foodRender;
        private readonly GrowTimerUI _growTimerUI;

        private float _timer;

        public GrowState(FoodBase foodBase, FoodData foodData, GameObject foodRender, GrowTimerUI growTimerUI)
        {
            _foodBase = foodBase;
            _foodData = foodData;
            _foodRender = foodRender;
            _growTimerUI = growTimerUI;
        }

        public void Enter()
        {
            _timer = _foodData.GrowTime;
            _growTimerUI.Init(_timer);
            _foodRender.transform.localScale = Vector3.zero;
            RandomModelRotation();
        }

        public void Update()
        {
            if (_timer > Mathf.Epsilon)
            {
                _timer -= Time.deltaTime;
                _foodRender.transform.localScale =
                    Vector3.Lerp(Vector3.zero, Vector3.one, 1f - _timer / _foodData.GrowTime);
            }
            else
            {
                _foodBase.ChangeState(_foodBase.RipeState);
            }
        }

        public void Exit()
        {
        }

        private void RandomModelRotation()
        {
            var randomRotation = Random.rotationUniform.eulerAngles.y;
            var rotationEulerAngles = _foodRender.transform.rotation.eulerAngles;
            rotationEulerAngles.y = randomRotation;
            _foodRender.transform.rotation = Quaternion.Euler(rotationEulerAngles);
        }
    }
}