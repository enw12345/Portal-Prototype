using UnityEngine;

namespace PortalGun
{
    public class PortalProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody _rigidbody;
        
        private void OnEnable()
        {
            _rigidbody.velocity = transform.forward * _speed;
        }
    }
}