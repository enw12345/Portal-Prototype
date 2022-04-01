using Manager;
using UnityEngine;

namespace CameraBehaviour
{
    public class CameraLook : MonoBehaviour
    {
        //Rotation
        [SerializeField] [Range(1, 5)] private float lookSpeed = 2f;
        [SerializeField] [Range(1, 100)] private float sensitivity = 100f;
        private float _xRotation;

        private void Update()
        {
            if (!GameManager.Instance.GameStart) return;
            Look();
        }

        private void Look()
        {
            var mouseY = Input.GetAxis("Mouse Y") * sensitivity * lookSpeed * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        }
    }
}