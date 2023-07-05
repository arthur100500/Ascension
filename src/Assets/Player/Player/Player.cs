using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Player
{
    public class Player : MonoBehaviour
    {
        public delegate void PlayerBounce();
        public event PlayerBounce OnPlayerBounce;
        
        public delegate void PlayerJump();
        public event PlayerJump OnPlayerJump;
        
        public delegate void PlayerGround();
        public event PlayerGround OnPlayerGround;
        
        public PlayerState state;
        public float jumpStatus;

        public Vector3 LastJumpAngle;

        public void InvokeGround()
        {
            OnPlayerGround?.Invoke();
        }
        
        public void InvokeJump()
        {
            OnPlayerJump?.Invoke();
        }
        
        public void InvokeBounce()
        {
            OnPlayerBounce?.Invoke();
        }
    }
}
