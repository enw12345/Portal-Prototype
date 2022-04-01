using System.Collections.Generic;
using InteractableObjects;
using Manager;
using UnityEngine;

namespace InteractBehaviour
{
    public abstract class InteractBehaviour : MonoBehaviour
    {
        private static IEnumerable<InteractableObject> _interactableObjectsList;
        protected static bool interacting;
        [Range(0.85f, 1f)] [SerializeField] private float _interactAccuracy;

        protected InteractableObject InteractingObject;
        [SerializeField] private float maxGrabDistance = 3f;
        
        private void Awake()
        {
            _interactableObjectsList = FindObjectsOfType<InteractableObject>();
            interacting = false;
        }

        private void Update()
        {
            if (!GameManager.Instance.GameStart) return;
            if (!Input.GetKeyDown(KeyCode.E)) return;

            if (!interacting)
            {
                if (!CheckInteraction()) return;
                Interact();
            }
            else if (interacting)
            {
                Interact();
            }
        }

        protected abstract void Interact();


        private bool CheckInteraction()
        {
            var isValid = false;

            foreach (var interactable in _interactableObjectsList)
                if (Vector3.Dot(transform.forward, (interactable.transform.position - transform.position).normalized) >=
                    _interactAccuracy)
                {
                    if (Vector3.Distance(interactable.transform.position, transform.position) < maxGrabDistance)
                    {
                        InteractingObject = interactable;
                        isValid = true;
                    }
                }

            return isValid;
        }
    }
}