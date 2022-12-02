using Farm._Scripts;
using Farm._Scripts.Items;
using UnityEngine;

namespace Farm.States
{
    public sealed class GrowState : IState
    {
        private readonly FoodLogic _foodLogic;
        private readonly FoodData _foodData;
        private readonly GameObject _foodModel;
        private readonly GrowTimerUI _growTimerUI;

        private float _timer;

        public GrowState(FoodLogic foodLogic, FoodData foodData, GameObject foodModel, GrowTimerUI growTimerUI)
        {
            _foodLogic = foodLogic;
            _foodData = foodData;
            _foodModel = foodModel;
            _growTimerUI = growTimerUI;
        }

        public void Enter()
        {
            _timer = _foodData.GrowDuration;
            _growTimerUI.SetDuration(_timer);
            _growTimerUI.Show();
            _foodModel.transform.localScale = Vector3.zero;
            RandomModelRotation();
        }

        public void Update()
        {
            if (_timer > Mathf.Epsilon)
            {
                _timer -= Time.deltaTime;
                _foodModel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, 1f - _timer / _foodData.GrowDuration);
            }
            else
            {
                _foodLogic.ChangeState(_foodLogic.RipeState);
            }
        }

        public void Exit()
        {
            _growTimerUI.Hide();
        }

        private void RandomModelRotation()
        {
            var randomRotation = Random.rotationUniform.eulerAngles.y;
            var rotationEulerAngles = _foodModel.transform.rotation.eulerAngles;
            rotationEulerAngles.y = randomRotation;
            _foodModel.transform.rotation = Quaternion.Euler(rotationEulerAngles);
        }
    }
}