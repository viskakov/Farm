using Farm._Scripts;
using UnityEngine;

namespace Farm.States
{
    public sealed class GrowState : IState
    {
        private readonly FoodLogic _foodLogic;
        private float _timer;
        private Vector3 _startScale = Vector3.zero;
        private Vector3 _targetScale = Vector3.one;

        public GrowState(FoodLogic foodLogic)
        {
            _foodLogic = foodLogic;
        }

        public void Enter()
        {
            RandomRotation();
            _foodLogic.FoodModel.transform.localScale = _startScale;
            _foodLogic.GrowTimerUI.SetDuration(_foodLogic.FoodData.GrowDuration);
            _foodLogic.GrowTimerUI.Show();
            _timer = _foodLogic.FoodData.GrowDuration;
        }

        public void Update()
        {
            if (_timer > Mathf.Epsilon)
            {
                _timer -= Time.deltaTime;
                _foodLogic.FoodModel.transform.localScale = Vector3.Lerp(_startScale, _targetScale, 1f - _timer / _foodLogic.FoodData.GrowDuration);
            }
            else
            {
                _foodLogic.ChangeState(_foodLogic.RipeState);
            }
        }

        public void Exit()
        {
            _foodLogic.GrowTimerUI.Hide();
        }

        private void RandomRotation()
        {
            var randomRotation = Random.rotationUniform.eulerAngles.y;
            var rotationEulerAngles = _foodLogic.FoodModel.transform.rotation.eulerAngles;
            rotationEulerAngles.y = randomRotation;
            _foodLogic.FoodModel.transform.rotation = Quaternion.Euler(rotationEulerAngles);
        }
    }
}