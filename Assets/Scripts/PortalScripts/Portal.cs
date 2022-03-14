using CharacterBehaviour;
using UnityEngine;

namespace PortalScripts
{
    public class Portal : MonoBehaviour
    {
        public Transform inPosition;
        [SerializeField] private Transform _outPosition;
        public Vector3 forwardDirection = Vector3.zero;
        private Portal _outPortal;
        public bool PortalIsActive;
        
        private void OnTriggerStay(Collider other)
        {
            if(!_outPortal.PortalIsActive) return;
            print("Teleporting: " + other.name);
            var objectRigidbody = other.GetComponent<Rigidbody>();
            var objectVelocityMag = objectRigidbody.velocity.magnitude;

            other.TryGetComponent(out FirstPersonController controller);
            
            TeleportObject(other.transform);
            objectRigidbody.velocity = other.transform.forward * objectVelocityMag;
        }

        public void Init(Portal outPortal, Transform outPosition)
        {
            _outPortal = outPortal;
            _outPosition = outPosition;
        }

        public void SetForwardDirection(Vector3 newForwardDirection)
        {
            forwardDirection = newForwardDirection;
        }
        
        private void TeleportObject(Transform objectTransform)
        {
            objectTransform.SetPositionAndRotation(_outPosition.position, Quaternion.FromToRotation(Vector3.forward, _outPortal.forwardDirection));
        }
    }
}