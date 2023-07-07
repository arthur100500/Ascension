using UnityEngine;

namespace Player.Player
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private AudioSource jumpAudio;
        [SerializeField] private AudioSource hitAudio;
        
        public void Start()
        {
            player.OnPlayerJump += PlayJump;
            player.OnPlayerBounce += PlayHit;
            player.OnPlayerGround += PlayHit;
        }

        private void PlayJump()
        {
            jumpAudio.Play(0);
        }
        
        private void PlayHit()
        {
            hitAudio.Play(0);
        }
    }
}