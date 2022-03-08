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
            _currentBluePortalProjectile.Init(_bluePortal, this);
            _currentOrangePortalProjectile.Init(_orangePortal, this);
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var bluePortal = _currentBluePortalProjectile.Portal;
                bluePortal.gameObject.SetActive(false);
                GetPortalSpawnLocation(bluePortal);
                
                _currentBluePortalProjectile.portalSpawned = false;
                _currentBluePortalProjectile.transform.SetPositionAndRotation(_portalSpawner.position,
                    _portalSpawner.rotation);
                _currentBluePortalProjectile.gameObject.SetActive(true);
                _currentBluePortalProjectile.GetComponent<Rigidbody>().velocity =
                    _portalSpawner.forward * _projectileSpeed;
            }

            if (Input.GetMouseButtonDown(1))
            {
                var orangePortal = _currentOrangePortalProjectile.Portal;
                orangePortal.gameObject.SetActive(false);
                GetPortalSpawnLocation(orangePortal);
                
                _currentOrangePortalProjectile.portalSpawned = false;
                _currentOrangePortalProjectile.transform.SetPositionAndRotation(_portalSpawner.position,
                    _portalSpawner.rotation);
                _currentOrangePortalProjectile.gameObject.SetActive(true);
                _currentOrangePortalProjectile.GetComponent<Rigidbody>().velocity =
                    _portalSpawner.forward * _projectileSpeed;
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(_portalSpawner.position, _portalSpawner.transform.forward);
        }

        private void GetPortalSpawnLocation(Portal portalToSpawn)
        {
            if (Physics.Raycast(_portalSpawner.position, _portalSpawner.transform.forward, out var hit,
                    Mathf.Infinity, _portalSurface))
            {
                var pos = hit.point + hit.normal * 0.1f;
                portalToSpawn.transform.SetPositionAndRotation(pos,
                    Quaternion.FromToRotation(Vector3.forward, -hit.normal));
                portalToSpawn.SetForwardDirection(hit.normal);
            }
        }
    }
}