using System;
using System.Collections.Generic;
using UnityEngine;
using InteractableObjects;

namespace InteractBehaviour
{
    public class InteractBehaviour : MonoBehaviour
    {
        [Range(0.85f,1f)] [SerializeField] private float _interactAccuracy;
        private IEnumerable<InteractableObject> _interactableObjectsList;

        private void Awake()
        {
            _interactableObjectsList = FindObjectsOfType<InteractableObject>();
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;
            foreach (var interactable in _interactableObjectsList)
            {
                if (Vector3.Dot(transform.forward,  (interactable.transform.position - transform.position).normalized) >= _interactAccuracy)
                {
                 interactable.Interact();
                }
            }
        }
    }
}