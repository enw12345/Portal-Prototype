using System.Collections.Generic;
using UnityEngine;
using InteractableObjects;

namespace InteractBehaviour
{
    public abstract class InteractBehaviour : MonoBehaviour
    {
        [Range(0.85f,1f)] [SerializeField] private float _interactAccuracy;
        private static IEnumerable<InteractableObject> _interactableObjectsList;

        protected InteractableObject InteractingObject;
        protected static bool interacting;
        
        private void Awake()
        {
            _interactableObjectsList = FindObjectsOfType<InteractableObject>();
            interacting = false;
        }
        
        protected abstract void Interact();
        
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;

            if (!interacting)
            {
                if (!CheckInteraction()) return;
                Interact();
                interacting = true;
            }
            else if(interacting)
            {
                Interact();
            }
        }


        private bool CheckInteraction()
        {
            var isValid = false;

            foreach (var interactable in _interactableObjectsList)
            {
                if (Vector3.Dot(transform.forward,  (interactable.transform.position - transform.position).normalized) >= _interactAccuracy)
                {
                    InteractingObject = interactable;
                    isValid = true;
                }
            }

            return isValid;
        }
    }
}