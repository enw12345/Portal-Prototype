using UnityEngine;

namespace PortalScripts
{
    public class PortalProjectile : MonoBehaviour
    {
        public bool portalSpawned;
        private Portal Portal { get; set; }
        public RaycastHit PortalHit { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Portal Surface") && !portalSpawned) return;
            SpawnPortal();
            
            gameObject.SetActive(false);
        }
        public void Init(Portal portalObject)
        {
            Portal = portalObject;
            Portal.PortalIsActive = false;
            Portal.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        
        private void SpawnPortal()
        {
            var pos = PortalHit.point + PortalHit.normal * 0.1f;
            Portal.transform.SetPositionAndRotation(pos,
                Quaternion.FromToRotation(Vector3.forward, -PortalHit.normal));
            Portal.SetForwardDirection(PortalHit.normal);
            
            Portal.gameObject.SetActive(true);
            portalSpawned = true;
            Portal.PortalIsActive = true;
        }
    }
}