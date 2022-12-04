using UnityEngine;

namespace Farm._Scripts.Helpers
{
    public static class VectorExtensions
    {
        public static Vector3 WithNewY(this Vector3 vector, float newY)
        {
            return new Vector3(vector.x, newY, vector.z);
        }
    }
}