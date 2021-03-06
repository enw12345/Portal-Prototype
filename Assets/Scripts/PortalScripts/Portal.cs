using CharacterBehaviour;
using UnityEngine;

namespace PortalScripts
{
    public class Portal : MonoBehaviour
    {
        private static Camera _playerCamera;
        public Transform inPosition;
        [SerializeField] private Transform _outPosition;
        public Vector3 forwardDirection = Vector3.zero;
        public bool PortalIsActive;

        [SerializeField] private Camera _portalCamera;
        private Portal _outPortal;
        private Camera _outPortalCamera;

        private void Awake()
        {
            _playerCamera = Camera.main;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_outPortal.PortalIsActive) return;
            var objectRigidbody = other.GetComponent<Rigidbody>();
            var objectVelocityMag = objectRigidbody.velocity.magnitude;
            
            other.GetComponent<Collider>().enabled = false;
            // if (!(Vector3.Distance(other.bounds.center, transform.position) < 0.1f)) return;
            TeleportObject(other.transform);
            other.transform.forward = -_outPortal.transform.forward;
            objectRigidbody.velocity = other.transform.forward * objectVelocityMag;
            objectRigidbody.rotation = Quaternion.FromToRotation(Vector3.forward, -_outPortal.forwardDirection);

            other.TryGetComponent(out FirstPersonController controller);
            if (!controller) return;
            var newRotationEuler = other.transform.rotation.eulerAngles;
            controller.ResetRotation(newRotationEuler.y);
        }

        private void UpdateCamera()
        {
            _portalCamera.fieldOfView = 120;
            var relativeRotation = transform.InverseTransformDirection(_playerCamera.transform.forward);
            relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
            _outPortalCamera.transform.forward = _outPortal.transform.TransformDirection(relativeRotation);

            // var relativePosition = transform.InverseTransformPoint(_playerCamera.transform.position);
            // relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
            // _outPortalCamera.transform.position = _outPortal.transform.TransformPoint(relativePosition);

            // var relativePosition = transform.InverseTransformPoint(_playerCamera.transform.position);
            // relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
            // _outPortalCamera.transform.position = _outPortal.transform.TransformPoint(relativePosition);
        }

        public void Init(Portal outPortal, Transform outPosition)
        {
            _outPortal = outPortal;
            _outPosition = outPosition;
            _outPortalCamera = _portalCamera;
        }

        public void SetForwardDirection(Vector3 newForwardDirection)
        {
            forwardDirection = newForwardDirection;
        }

        private void TeleportObject(Transform objectTransform)
        {
            objectTransform.SetPositionAndRotation(_outPosition.position,
                Quaternion.FromToRotation(Vector3.forward, -_outPortal.forwardDirection));
            objectTransform.GetComponent<Collider>().enabled = true;
        }
    }
}