using System;
using UnityEngine;

namespace InteractableObjects
{
    public class GrabbableObject : InteractableObject
    {
        private static bool _isGrabbing;
        public Transform grabPosition;
        private float _angularSpeed;
        
        public override void Interact()
        {
            if(!_isGrabbing)
                Grab();
        }

        private void Update()
        {
            if (!_isGrabbing) return;
            
            if(Input.GetKey(KeyCode.E) && _isGrabbing)
                Drop();
            transform.position = grabPosition.position;
        }

        private void Drop()
        {
            print("Dropping");
            _isGrabbing = false;
            var rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.angularDrag = _angularSpeed;
        }

        private void Grab()
        {
            print("Grabbing");
            _isGrabbing = true;
            var rb = GetComponent<Rigidbody>();

            _angularSpeed = rb.angularDrag;
            rb.useGravity = false;

            rb.angularDrag = 1;
        }

    }
}