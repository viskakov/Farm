using UnityEngine;

namespace Farm.Helpers
{
    public static class AnimatorHash
    {
        public static readonly int Walking = Animator.StringToHash("Walking");
        public static readonly int Plant = Animator.StringToHash("Plant");
        public static readonly int Pickup = Animator.StringToHash("Pickup");
    }
}