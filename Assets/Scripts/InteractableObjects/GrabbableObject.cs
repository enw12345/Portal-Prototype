using UnityEngine;

namespace InteractableObjects
{
    public class GrabbableObject : InteractableObject
    {
        public Transform grabPosition;
        private float _angularSpeed;
        private bool _grabbed;

        private void Update()
        {
            if (!_grabbed) return;

            transform.position = grabPosition.position;
        }

        public override void Interact()
        {
            if (!_grabbed)
                Grab();
            else
                Drop();
        }


        private void Drop()
        {
            _grabbed = false;
            GetComponent<Rigidbody>().useGravity = true;
        }

        private void Grab()
        {
            _grabbed = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }
}