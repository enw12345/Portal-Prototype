using UnityEngine;

namespace PortalGun
{
    public class PortalGunBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _portalSpawner;
        [SerializeField] private PortalProjectile _bluePortalProjectile;
        [SerializeField] private PortalProjectile _orangePortalProjectile;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire(_bluePortalProjectile);
            }

            if (Input.GetMouseButtonDown(1))
            {
                Fire(_orangePortalProjectile);
            }
        }

        private void Fire(PortalProjectile projectile)
        {
            Instantiate(projectile, _portalSpawner.position, _portalSpawner.rotation);
        }
    }
}