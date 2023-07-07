using System;
using UnityEngine;

namespace Player.Camera
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float sens;
        [SerializeField] private LayerMask playerLayer;

        [SerializeField] private float preferredDistance = 3;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var playerTransform = player.transform;
            var playerPos = playerTransform.position;
            var myTransform = transform;

            if (
                Physics.Raycast(playerPos, -myTransform.forward, out var hit, Mathf.Infinity, ~ playerLayer)
                && (preferredDistance) > (playerPos - hit.point).magnitude
            ) myTransform.position = hit.point + hit.normal * 0.1f;
            else
                myTransform.position = playerPos - (myTransform.forward) * preferredDistance;
            

            transform.RotateAround(playerPos, 
                Vector3.up,
                Input.GetAxis("Mouse X") * sens
            );

            transform.RotateAround(playerPos, 
                myTransform.right,
                -Input.GetAxis("Mouse Y") * sens
            );

            var scroll = Input.mouseScrollDelta.y;
            
            preferredDistance -= scroll;
            preferredDistance = MathF.Max(preferredDistance, 1);
        }
    }
}
