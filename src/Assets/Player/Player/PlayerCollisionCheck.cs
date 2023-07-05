using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player.Player
{
    public class PlayerCollisionCheck : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Rigidbody playerBody;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float bounceMultiplier;
        

        private Vector3 _frameBeforeSpeed;

        private void Update()
        {
            _frameBeforeSpeed = playerBody.velocity;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            DrawDebugPoints(other.contacts);

            var orientation = GetDebugOrientation(other.contacts);

            switch (orientation)
            {
                case ColliderOrientation.Floor:
                    if (1 << other.gameObject.layer != groundLayer || player.state == PlayerState.Preparing) return;
                    player.state = PlayerState.Grounded;
                    player.InvokeGround();
                    playerBody.velocity = Vector3.zero;
                    break;
                
                case ColliderOrientation.Wall:
                    if (other.contactCount < 3) return;
                    
                    var normal = GetNormal(
                        other.GetContact(0).point,
                        other.GetContact(1).point,
                        other.GetContact(2).point
                    );

                    var myPosition = transform.position;
                    Debug.DrawLine(myPosition, myPosition + normal, Color.white, 1);
                    
                    var reversedSpeed = Vector3.Reflect(_frameBeforeSpeed, normal);

                    reversedSpeed = new Vector3(
                        reversedSpeed.x * bounceMultiplier, 
                        reversedSpeed.y, 
                        reversedSpeed.z * bounceMultiplier
                    );
                    
                    Debug.DrawLine(myPosition, myPosition + reversedSpeed, Color.red, 1);
                    Debug.DrawLine(myPosition, myPosition + _frameBeforeSpeed, Color.blue, 1);

                    playerBody.velocity = reversedSpeed;
                    
                    player.InvokeBounce();
                    
                    break;
                
                case ColliderOrientation.Slide:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawDebugPoints(IEnumerable<ContactPoint> points)
        {
            var contactPoints = points as ContactPoint[] ?? points.ToArray();

            for (var i = 1; i < contactPoints.Length - 1; i++)
                Debug.DrawLine(contactPoints[i].point, contactPoints[i - 1].point);
            
            Debug.DrawLine(contactPoints[0].point, contactPoints[^1].point);
        }

        private ColliderOrientation GetDebugOrientation(IEnumerable<ContactPoint> points)
        {
            var contactPoints = points as ContactPoint[] ?? points.ToArray();
            var count = contactPoints.Count();
            
            if (count == 2)
                return ColliderOrientation.Slide;

            return contactPoints
                .Skip(1)
                .Any(point => Math.Abs(point.point.y - contactPoints.First().point.y) > 0.01f) ?
                ColliderOrientation.Wall 
                : ColliderOrientation.Floor;
        }

        private Vector3 GetNormal(Vector3 first, Vector3 second, Vector3 third)
        {
            var plane = new Plane(first, second, third);
 
            return plane.normal;
        }
    }
}