using UnityEngine;

namespace TacticalFrontline.Player
{
    public sealed class ThirdPersonCameraRig : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 shoulderOffset = new Vector3(0.55f, 1.65f, -3.4f);
        [SerializeField] private float sensitivity = 2.2f;
        [SerializeField] private float minPitch = -35f;
        [SerializeField] private float maxPitch = 70f;
        [SerializeField] private float followSharpness = 18f;
        [SerializeField] private LayerMask obstructionMask = ~0;
        private float yaw;
        private float pitch = 12f;

        private void LateUpdate()
        {
            if (target == null) return;
            if (Application.isFocused && Input.GetMouseButton(1))
            {
                yaw += Input.GetAxis("Mouse X") * sensitivity * 4f;
                pitch -= Input.GetAxis("Mouse Y") * sensitivity * 4f;
                pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            }

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
            Vector3 pivot = target.position + Vector3.up * 1.35f;
            Vector3 desired = pivot + rotation * shoulderOffset;
            Vector3 ray = desired - pivot;
            if (Physics.SphereCast(pivot, 0.18f, ray.normalized, out RaycastHit hit,
                    ray.magnitude, obstructionMask, QueryTriggerInteraction.Ignore))
                desired = pivot + ray.normalized * Mathf.Max(0.25f, hit.distance - 0.08f);

            float blend = 1f - Mathf.Exp(-followSharpness * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, desired, blend);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(pivot - transform.position), blend);
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            if (target != null) yaw = target.eulerAngles.y;
        }
    }
}
