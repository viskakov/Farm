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

        private void OnEnable()
        {
            PlayerMovement.OnReachPosition += TriggerPlantAnimation;
        }

        private void OnDisable()
        {
            PlayerMovement.OnReachPosition -= TriggerPlantAnimation;
        }

        private void Update()
        {
            var isMoving = PlayerMovement.Instance.IsMoving;
            _animator.SetBool(AnimatorHash.Walking, isMoving);
        }

        private void TriggerPlantAnimation()
        {
            _animator.SetTrigger(AnimatorHash.Plant);
        }
    }
}