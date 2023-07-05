using UnityEngine;

namespace Player.Player
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private AudioSource playerAudio;
        
        public void Start()
        {
            player.OnPlayerJump += PlayJump;
        }

        private void PlayJump()
        {
            playerAudio.Play(0);
        }
    }
}