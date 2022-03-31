using System.Collections;
using UnityEngine;

namespace DoorsSwitches
{
    public class DoorBehaviour : MonoBehaviour
    {

        [SerializeField] private float openYPosition = 4.2f;
        [SerializeField] private float closeYPosition = -0.6000004f;
        [SerializeField] private float openTime = 0.1f;
        [SerializeField] private float doorSpeed = 0.2f;

        private float t;
        // Start is called before the first frame update

        public IEnumerator Open(Vector3 startPosition)
        {
            while (transform.localPosition.y < openYPosition)
            {
                var newPosition = new Vector3(transform.localPosition.x, openYPosition, transform.localPosition.z);
                transform.localPosition = Vector3.Lerp(startPosition, newPosition, t += Time.deltaTime * doorSpeed);
                yield return new WaitForSeconds(openTime);
            }
        }

        public IEnumerator Close(Vector3 startPosition)
        {
            while (transform.localPosition.y > closeYPosition)
            {
                var newPosition = new Vector3(transform.localPosition.x, closeYPosition, transform.localPosition.z);
                transform.localPosition = Vector3.Lerp(startPosition, newPosition, t += Time.deltaTime * doorSpeed);
                yield return new WaitForSeconds(openTime);
            }
        }
    }
}
