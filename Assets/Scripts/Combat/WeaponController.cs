using System.Collections;
using UnityEngine;
using TacticalFrontline.Combat;

namespace TacticalFrontline.Weapons
{
    public sealed class WeaponController : MonoBehaviour
    {
        [Header("Weapon")]
        [SerializeField] private float damage = 24f;
        [SerializeField] private float range = 120f;
        [SerializeField] private float shotsPerSecond = 8f;
        [SerializeField] private int magazineSize = 30;
        [SerializeField] private float reloadSeconds = 1.8f;
        [SerializeField] private LayerMask hitMask = ~0;
        [SerializeField] private Transform muzzle;

        public int AmmoInMagazine { get; private set; }
        public int ReserveAmmo { get; private set; } = 120;
        public bool IsReloading { get; private set; }
        private float nextShotTime;

        private void Awake() => AmmoInMagazine = magazineSize;

        public bool TryFire(Camera aimCamera, GameObject owner)
        {
            if (aimCamera == null || IsReloading || AmmoInMagazine <= 0 ||
                Time.time < nextShotTime) return false;

            nextShotTime = Time.time + 1f / Mathf.Max(0.1f, shotsPerSecond);
            AmmoInMagazine--;

            Vector3 origin = aimCamera.transform.position;
            Vector3 direction = aimCamera.transform.forward;
            if (Physics.Raycast(origin, direction, out RaycastHit hit, range, hitMask,
                    QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.GetComponentInParent<IDamageable>() is IDamageable target)
                    target.TakeDamage(damage, owner);
            }
            return true;
        }

        public void BeginReload()
        {
            if (!IsReloading && AmmoInMagazine < magazineSize && ReserveAmmo > 0)
                StartCoroutine(ReloadRoutine());
        }

        private IEnumerator ReloadRoutine()
        {
            IsReloading = true;
            yield return new WaitForSeconds(reloadSeconds);
            int needed = magazineSize - AmmoInMagazine;
            int loaded = Mathf.Min(needed, ReserveAmmo);
            AmmoInMagazine += loaded;
            ReserveAmmo -= loaded;
            IsReloading = false;
        }
    }
}
