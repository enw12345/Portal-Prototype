using UnityEngine;

namespace InteractableObjects
{
    public class GrabbableObject : InteractableObject
    {
        private bool _grabbed;
        public Transform grabPosition;
        private float _angularSpeed;
        
        public override void Interact()
        {
            if(!_grabbed)
                Grab();
            else
            {
                Drop();
            }
        }

        private void Update()
        {
            if (!_grabbed) return;

            transform.position = grabPosition.position;
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