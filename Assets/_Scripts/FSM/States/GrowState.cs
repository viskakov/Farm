using Farm._Scripts;
using UnityEngine;

namespace Farm.States
{
    public sealed class GrowState : IState
    {
        private readonly Context _context;
        private float _timer;
        private Vector3 _startScale = Vector3.zero;
        private Vector3 _targetScale = Vector3.one;

        public GrowState(Context context)
        {
            _context = context;
        }

        public void Enter()
        {
            RandomRotation();
            _context.transform.localScale = _startScale;
            _timer = _context.ItemToGrowData.GrowDuration;
        }

        public void Update()
        {
            if (_timer > Mathf.Epsilon)
            {
                _timer -= Time.deltaTime;
                _context.transform.localScale = Vector3.Lerp(_startScale, _targetScale, 1f - _timer / _context.ItemToGrowData.GrowDuration);
            }
            else
            {
                _context.ChangeState(_context.RipeState);
            }
        }

        public void Exit()
        {
        }

        private void RandomRotation()
        {
            var randomRotation = Random.rotationUniform.eulerAngles.y;
            var rotationEulerAngles = _context.transform.rotation.eulerAngles;
            rotationEulerAngles.y = randomRotation;
            _context.transform.rotation = Quaternion.Euler(rotationEulerAngles);
        }
    }
}