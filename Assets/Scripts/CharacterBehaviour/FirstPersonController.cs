using UnityEngine;

namespace CharacterBehaviour
{
    public class FirstPersonController : MonoBehaviour
    {
        public LayerMask groundLayer;

        //Movement
        [SerializeField] [Range(1, 25)] private float moveSpeed = 12f;
        [SerializeField] [Range(1, 5)] private float lookSpeed = 2f;


        [SerializeField] private Transform stepRayHigher;
        [SerializeField] private Transform stepRayLower;
        [SerializeField] private float stepHeight = 0.11f;
        [SerializeField] private float stepSmooth = 0.1f;
        public float playerGravity = 5f;

        private CapsuleCollider _collider;
        private Rigidbody _rb;

        private float _yRotation;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            stepRayHigher.position = new Vector3(stepRayHigher.position.x, stepHeight, stepRayHigher.position.z);
            Cursor.lockState = CursorLockMode.Locked;
            Input.ResetInputAxes();
        }

        private void FixedUpdate()
        {
            Turn();
            Move();
            Gravity();
            StepClimb();
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

        private void Turn()
        {
            var mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            if (mouseX == 0) return;

            _yRotation += mouseX;

            var desiredRotation = Quaternion.Euler(0, _yRotation, 0);
            _rb.rotation = desiredRotation;
        }

        public void ResetRotation(float newY)
        {
            _yRotation = newY;
        }
        
        private bool IsGrounded()
        {
            var colliderBounds = _collider.bounds;
            var raycastHit = Physics.Raycast(colliderBounds.center, Vector3.down, colliderBounds.extents.y + 0.1f,
                groundLayer);
            return raycastHit;
        }

        private void StepClimb()
        {
            Debug.DrawRay(stepRayLower.position, transform.forward, Color.red);
            Debug.DrawRay(stepRayHigher.position, transform.forward, Color.blue);

            RaycastHit hitLower;
            if (Physics.Raycast(stepRayLower.position, transform.TransformDirection(Vector3.forward), out hitLower, .1f,
                    groundLayer))
            {
                print("Found Step");
                RaycastHit hitHigher;
                if (!Physics.Raycast(stepRayHigher.position, transform.TransformDirection(Vector3.forward),
                        out hitHigher, 0.2f, groundLayer))
                {
                    print("Stepping");
                    _rb.position -= new Vector3(0, -stepSmooth, 0);
                    // _rb.position += new Vector3(_rb.position.x, _rb.position.y + stepSmooth, _rb.position.z);
                }
            }
        }
    }
}