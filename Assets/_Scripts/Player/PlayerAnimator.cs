using UnityEngine;

namespace TreasureHunter
{
    public sealed class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int Walking = Animator.StringToHash("Walking");

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            var isMoving = PlayerMovement.Instance.IsMoving;
            _animator.SetBool(Walking, isMoving);
        }
    }
}