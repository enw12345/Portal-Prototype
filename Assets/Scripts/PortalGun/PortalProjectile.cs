using UnityEngine;

namespace PortalGun
{
    public class PortalProjectile : MonoBehaviour
    {
        public bool portalSpawned;
        private PortalGunBehaviour _portalGun;
        public Portal Portal { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Portal Surface")) return;
            SpawnPortal();
        }

        public void Init(Portal portalObject, PortalGunBehaviour portalGun)
        {
            Portal = portalObject;
            Portal.gameObject.SetActive(false);
            _portalGun = portalGun;
            gameObject.SetActive(false);
            portalSpawned = false;
        }

        private void SpawnPortal()
        {
            Portal.gameObject.SetActive(true);
            portalSpawned = true;
        }
    }
}