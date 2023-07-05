using System;
using UnityEngine;

namespace Player.Camera
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float sens;

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
            
            transform.RotateAround(playerPos, 
                Vector3.up,
                Input.GetAxis("Mouse X") * sens
            );

            transform.RotateAround(playerPos, 
                myTransform.right,
                -Input.GetAxis("Mouse Y") * sens
            );
        }
    }
}
