using System;
using UnityEngine;

namespace Player.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float jumpTime;
        private Rigidbody _body;
        [SerializeField] private Player player;
        
        [SerializeField] private float jumpAngle;
        [SerializeField] private float jumpStrength;
        [SerializeField] private float maxJumpStrength;
        [SerializeField] private float minJumpStrength;
        [SerializeField] private float jumpAccumulationSpeed;
        [SerializeField] private Transform cameraObject;

        private void Start()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void Update()
        {

            if (Input.GetKey(KeyCode.Space) && player.state != PlayerState.InAir)
            {
                jumpTime += jumpAccumulationSpeed * Time.deltaTime;

                jumpTime = MathF.Min(jumpTime, maxJumpStrength);
                
                player.jumpStatus = jumpTime / maxJumpStrength;
            }

            if (Input.GetKey(KeyCode.Space) && player.state == PlayerState.Grounded)
            {
                jumpTime = minJumpStrength;
                
                player.state = PlayerState.Preparing;
            }

            if (Input.GetKeyUp(KeyCode.Space) && player.state == PlayerState.Preparing)
            {
                Jump();
                
                player.state = PlayerState.InAir;
                
                jumpTime = 0;
            }
        }

        private void Jump()
        {
            if (jumpTime < minJumpStrength)
                return;

            var rotation = cameraObject.rotation.eulerAngles;

            var quaternionRotation = Quaternion.Euler(jumpAngle, rotation.y, 0);

            var jump = jumpStrength * (MathF.Min(jumpTime, maxJumpStrength)) * Vector3.forward;
            
            _body.velocity = quaternionRotation * jump;

            player.LastJumpAngle = rotation;
            
            player.InvokeJump();
        }
    }
}
