using UnityEngine;

namespace CharacterBehaviour
{
    public class FirstPersonController : MonoBehaviour
    {
        public LayerMask groundLayer;

        //Movement
        [SerializeField] [Range(1, 25)] private float moveSpeed = 12f;
        [SerializeField] [Range(1, 5)] private float lookSpeed = 2f;

        public float playerGravity = 5f;

        public bool isTeleporting;

        private CapsuleCollider _collider;
        private Rigidbody _rb;

        private float _yRotation;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            Look();
            Move();
            Gravity();
        }

        private void Gravity()
        {
            if (!IsGrounded()) _rb.AddForce(Physics.gravity * playerGravity);
        }

        private void Move()
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            var move = transform.right * x + transform.forward * z;
            var desiredVelocity = move * moveSpeed;
            _rb.velocity = desiredVelocity;
        }

        private void Look()
        {
            var mouseX = Input.GetAxis("Mouse X") * lookSpeed;

            _yRotation += mouseX;

            var desiredRotation = Quaternion.Euler(0, _yRotation, 0);
            _rb.rotation = desiredRotation;
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