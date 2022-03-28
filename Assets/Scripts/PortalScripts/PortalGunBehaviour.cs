using UnityEngine;

namespace PortalScripts
{
    public class PortalGunBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _portalSpawner;
        [SerializeField] private PortalProjectile _bluePortalProjectile;
        [SerializeField] private PortalProjectile _orangePortalProjectile;

        [SerializeField] private Portal _bluePortal;
        [SerializeField] private Portal _orangePortal;

        [SerializeField] private float _projectileSpeed;

        [SerializeField] private LayerMask _portalSurface;

        private PortalProjectile _currentBluePortalProjectile;
        private PortalProjectile _currentOrangePortalProjectile;

        private void Awake()
        {
            _bluePortal = Instantiate(_bluePortal, Vector3.zero, Quaternion.identity);
            _orangePortal = Instantiate(_orangePortal, Vector3.zero, Quaternion.identity);
            
            _bluePortal.Init(_orangePortal, _orangePortal.inPosition);
            _orangePortal.Init(_bluePortal, _bluePortal.inPosition);

            _currentBluePortalProjectile = Instantiate(_bluePortalProjectile, Vector3.zero, Quaternion.identity);
            _currentOrangePortalProjectile = Instantiate(_orangePortalProjectile, Vector3.zero, Quaternion.identity);
            _currentBluePortalProjectile.Init(_bluePortal);
            _currentOrangePortalProjectile.Init(_orangePortal);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetPortalSpawnLocation(_currentBluePortalProjectile);
                ShootProjectile(_currentBluePortalProjectile);
            }

            if (!Input.GetMouseButtonDown(1)) return;
            GetPortalSpawnLocation(_currentOrangePortalProjectile);
            ShootProjectile(_currentOrangePortalProjectile);
        }

        private void ShootProjectile(PortalProjectile projectile)
        {
            projectile.portalSpawned = false;
            projectile.transform.SetPositionAndRotation(_portalSpawner.position, _portalSpawner.rotation);
            
            projectile.gameObject.SetActive(true);
            projectile.GetComponent<Rigidbody>().velocity = _portalSpawner.forward * _projectileSpeed;
        }
        
        private void GetPortalSpawnLocation(PortalProjectile projectile)
        {
            Physics.Raycast(_portalSpawner.position, _portalSpawner.transform.forward, out var hit,
                Mathf.Infinity, _portalSurface);
            projectile.PortalHit = hit;
        }
    }
}