using Manager;
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
        
        private Rigidbody _rb;

        private float _yRotation;

        [SerializeField]private bool isGrounded;
        
        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            stepRayHigher.position = new Vector3(stepRayHigher.position.x, stepHeight, stepRayHigher.position.z);
            Cursor.lockState = CursorLockMode.Locked;
            Input.ResetInputAxes();
            groundLayer |= (1 << 6);
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.GameStart) return;
            Turn();
            Move();
            StepClimb();
            Gravity();
            isGrounded = false;
        }

        private void OnCollisionStay(Collision collision)
        {
            var collisionLayer = collision.gameObject.layer;
                var layerAsLayerMask = (1 << collisionLayer);
            if ( layerAsLayerMask == groundLayer)
                isGrounded = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            var collisionLayer = collision.gameObject.layer;
            var layerAsLayerMask = (1 << collisionLayer);
            
            if (layerAsLayerMask == groundLayer)
                isGrounded = false;
        }

        private void Gravity()
        {
            if (!isGrounded) _rb.AddForce(Physics.gravity * playerGravity);
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