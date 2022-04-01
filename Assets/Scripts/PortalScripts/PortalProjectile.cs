using UnityEngine;

namespace PortalScripts
{
    public class PortalProjectile : MonoBehaviour
    {
        private Portal Portal { get; set; }
        public RaycastHit PortalHit { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Portal Surface")) SpawnPortal();

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

            var downRay = new Ray(pos, Vector3.down);
            var upRay = new Ray(pos, Vector3.up);
            
            var leftRay = new Ray(pos, Vector3.left);
            var rightRay = new Ray(pos, Vector3.right);
            
            Physics.Raycast(downRay, out var downHitInfo);
            Physics.Raycast(upRay, out var upHitInfo);
            var portalBounds = Portal.gameObject.GetComponent<Collider>().bounds;
            var portalSize = portalBounds.size.magnitude;

            var downDistance = Vector3.Distance(pos, downHitInfo.point);
            var upDistance = Vector3.Distance(pos, upHitInfo.point);
            if (downDistance < portalSize && upDistance < portalSize)
            { 
                var newY = (downHitInfo.point + upHitInfo.point) * 0.5f;
                
                pos = new Vector3(pos.x, newY.y, pos.z);
            }
            else if (downDistance < portalSize)
            {
                pos = new Vector3(pos.x, pos.y + portalBounds.extents.y, pos.z);
            }
            else if (upDistance < portalSize)
            {
                pos = new Vector3(pos.x, pos.y - portalBounds.extents.y, pos.z);
            }

            Portal.transform.SetPositionAndRotation(pos,
                Quaternion.FromToRotation(Vector3.forward, -PortalHit.normal));
            Portal.SetForwardDirection(PortalHit.normal);

            Portal.gameObject.SetActive(true);
            Portal.PortalIsActive = true;
        }
    }
}