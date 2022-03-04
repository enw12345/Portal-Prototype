using UnityEngine;

namespace CharacterBehaviour
{
    public class FirstPersonController : MonoBehaviour
    {
        public LayerMask groundLayer;

        //Movement
        [SerializeField] private CharacterController controller;
        [SerializeField] [Range(1, 25)] private float moveSpeed = 12f;

        [SerializeField] [Range(1, 5)] private float lookSpeed = 2f;
        [SerializeField] [Range(1, 100)] private float sensitivity = 100f;

        private CapsuleCollider _collider;
        private Transform _transform;

        private float _yRotation;

        private Vector3 velocity;

        // Start is called before the first frame update
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _collider = GetComponent<CapsuleCollider>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
            Look();
        }

        private void FixedUpdate()
        {
        }

        private void Move()
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            var move = _transform.right * x + _transform.forward * z;
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        private void Look()
        {
            var mouseX = Input.GetAxis("Mouse X") * sensitivity * lookSpeed * Time.deltaTime;

            _yRotation += mouseX;
            transform.localRotation = Quaternion.Euler(0, _yRotation, 0);
        }

        private bool IsGrounded()
        {
            var colliderBounds = _collider.bounds;
            var raycastHit = Physics.Raycast(colliderBounds.center, Vector3.down, colliderBounds.extents.y + 0.1f,
                groundLayer);
            var rayColor = raycastHit ? Color.green : Color.red;
            Debug.DrawRay(colliderBounds.center, Vector3.down * (colliderBounds.extents.y + 1), rayColor);
            return raycastHit;
        }
    }
}