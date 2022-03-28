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

        private void Update()
        {
            UpdateCamera();
        }

        private void OnTriggerExit(Collider other)
        {
            other.GetComponent<Collider>().enabled = true;

            other.TryGetComponent(out FirstPersonController controller);
            if (controller)
                controller.enabled = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_outPortal.PortalIsActive) return;
            var objectRigidbody = other.GetComponent<Rigidbody>();
            var objectVelocityMag = objectRigidbody.velocity.magnitude;

            TeleportObject(other.transform);

            other.transform.forward = _outPortal.transform.forward;
            objectRigidbody.velocity = other.transform.forward * objectVelocityMag;
            other.GetComponent<Collider>().enabled = false;
        }

        private void UpdateCamera()
        {
            _portalCamera.fieldOfView = 60;
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

            print(_portalCamera.pixelWidth + " , " + _portalCamera.pixelHeight);
        }

        public void SetForwardDirection(Vector3 newForwardDirection)
        {
            forwardDirection = newForwardDirection;
        }

        private void TeleportObject(Transform objectTransform)
        {
            objectTransform.SetPositionAndRotation(_outPosition.position,
                Quaternion.FromToRotation(Vector3.forward, _outPortal.forwardDirection));

            objectTransform.TryGetComponent(out FirstPersonController controller);
            if (controller) controller.enabled = false;
        }
    }
}