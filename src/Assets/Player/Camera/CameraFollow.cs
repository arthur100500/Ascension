using System;
using UnityEngine;

namespace Player.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform objectToFollow;
        [SerializeField] private float speedRatio;
        [SerializeField] private float threshold;

        private void Update()
        {
            var o = gameObject;

            if (MathF.Abs(o.transform.position.y - objectToFollow.transform.position.y) < threshold)
                return;

            var myPosition = o.transform.position;
            var otherPosition = objectToFollow.transform.position;

            var newVec = (myPosition + otherPosition * speedRatio) / (1 + speedRatio);

            o.transform.position = newVec;
        }
    }
}
