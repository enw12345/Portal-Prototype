using System;
using System.Collections;
using UnityEngine;

namespace CharacterBehaviour
{
    public class FirstPersonController : MonoBehaviour
    {
        public LayerMask groundLayer;
        //Movement
        [SerializeField] [Range(1, 25)] private float moveSpeed = 12f;

        [SerializeField] [Range(1, 5)] private float lookSpeed = 2f;
        [SerializeField] [Range(1, 100)] private float sensitivity = 100f;

        private CapsuleCollider _collider;

        private float _yRotation;
        private Rigidbody _rb;

        public float playerGravity = 5f;
        
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
            if (!IsGrounded())
            {
                _rb.AddForce(Physics.gravity * playerGravity);
            }
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