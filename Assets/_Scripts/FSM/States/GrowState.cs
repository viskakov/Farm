using Farm._Scripts;
using UnityEngine;

namespace Farm.States
{
    public sealed class GrowState : IState
    {
        private readonly Food _food;
        private float _timer;
        private Vector3 _startScale = Vector3.zero;
        private Vector3 _targetScale = Vector3.one;

        public GrowState(Food food)
        {
            _food = food;
        }

        public void Enter()
        {
            RandomRotation();
            _food.transform.localScale = _startScale;
            _timer = _food.ItemToGrowData.GrowDuration;
        }

        public void Update()
        {
            if (_timer > Mathf.Epsilon)
            {
                _timer -= Time.deltaTime;
                _food.transform.localScale = Vector3.Lerp(_startScale, _targetScale, 1f - _timer / _food.ItemToGrowData.GrowDuration);
            }
            else
            {
                _food.ChangeState(_food.RipeState);
            }
        }

        public void Exit()
        {
        }

        private void RandomRotation()
        {
            var randomRotation = Random.rotationUniform.eulerAngles.y;
            var rotationEulerAngles = _food.transform.rotation.eulerAngles;
            rotationEulerAngles.y = randomRotation;
            _food.transform.rotation = Quaternion.Euler(rotationEulerAngles);
        }
    }
}