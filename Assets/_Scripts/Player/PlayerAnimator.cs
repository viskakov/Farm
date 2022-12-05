using Farm._Scripts.Helpers;
using UnityEngine;

namespace TreasureHunter
{
    public sealed class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            var isMoving = PlayerMovement.Instance.IsMoving;
            _animator.SetBool(AnimatorHash.Walking, isMoving);
        }

        public void PlayPlantAnimation()
        {
            _animator.SetTrigger(AnimatorHash.Plant);
        }
    }
}