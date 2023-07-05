using UnityEngine;

namespace Player.Player
{
    public class ModelLookAt : MonoBehaviour
    {
        [SerializeField] private Player player;
        
        private void Update()
        {
            transform.rotation = Quaternion.Euler(0, player.LastJumpAngle.y, 0);
        }
    }
}
