using System;
using UnityEngine;

namespace CharacterController
{
    public class ThirdPersonController : MonoBehaviour
    {
        [SerializeField] [Range(1,5)] private int speed = 2;

        [SerializeField] [Range(1, 100)] private float sensitivity = 100f;

        private float _xRotation = 0f;

        private float _yRotation = 0f;
        
        // Start is called before the first frame update
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
            Look();
        }

        private void Move()
        {
            if (Input.GetKey(KeyCode.W))
            {
                var position = transform.position;
                position += transform.forward * (speed * Time.deltaTime);
                transform.position = position;
            }
            
            else if (Input.GetKey(KeyCode.S))
            {
                var position = transform.localPosition;
                position -= transform.forward * (speed * Time.deltaTime);
                transform.localPosition = position;
            }
        }

        private void Look()
        {
            var mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            _yRotation += mouseX;
            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        }
    }
}
