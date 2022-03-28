using System;
using UnityEngine;

namespace DoorsSwitches
{
    public class ButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private DoorBehaviour door;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Portal Cube"))
            {
               StartCoroutine( door.Open(door.transform.localPosition));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Portal Cube"))
            {
                StartCoroutine(door.Close(door.transform.localPosition));
            }
        }
    }
}