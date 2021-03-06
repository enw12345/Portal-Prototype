using UnityEngine;

namespace InteractableObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public abstract void Interact();

        public virtual void CancelInteraction()
        {
        }
    }
}