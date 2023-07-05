using System;
using UnityEngine;

namespace Player.Player
{
    public class PlayerAnimate : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Transform mesh;
        [SerializeField] private float transformSpeed;

        private Vector3 _preferredScale;
        private Vector3 _preferredTransform;
        
        private void Update()
        {
            SetPreferredState();

            GoToPreferredState();

        }

        private void GoToPreferredState()
        {
            mesh.localScale = (mesh.localScale + _preferredScale * transformSpeed) / (1 + transformSpeed);
            mesh.localPosition = (mesh.localPosition + _preferredTransform * transformSpeed) / (1 + transformSpeed);
        }
        
        
        private void SetPreferredState()
        {
            switch (player.state)
            {
                case PlayerState.Preparing:
                    var width = 1 + player.jumpStatus * 0.4f;
                    var height = 1 - player.jumpStatus * 0.4f;
                    _preferredScale = new Vector3(width, height, width);

                    var localPosition = mesh.localPosition;
                    _preferredTransform = new Vector3(localPosition.x, 0.5f - width / 2, localPosition.z);
                    break;
                
                
                case PlayerState.InAir:
                    _preferredScale = new Vector3(0.9f, 1.15f, 0.9f);

                    _preferredTransform = Vector3.zero;
                    break;
                
                case PlayerState.Grounded:
                    _preferredScale = new Vector3(1f, 1f, 1f);
                    
                    _preferredTransform = Vector3.zero;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
