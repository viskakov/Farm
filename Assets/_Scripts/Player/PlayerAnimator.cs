using Farm._Scripts.Helpers;
using UnityEngine;

namespace Farm._Scripts.Player
{
    public sealed class PlayerAnimator : MonoBehaviour
    {
        public Animator Animator { get; private set; }

        private void Awake()
        {
            Animator = GetComponentInChildren<Animator>();
        }

        public void PlayWalkAnimation(bool isMoving)
        {
            Animator.SetBool(AnimatorHash.Walking, isMoving);
        }

        public void PlayPlantAnimation()
        {
            Animator.SetTrigger(AnimatorHash.Plant);
        }
    }
}