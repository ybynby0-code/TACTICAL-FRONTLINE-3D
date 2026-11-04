using System.Collections;
using UnityEngine;

namespace TacticalFrontline.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5.5f;
        [SerializeField] private float gravity = -22f;
        [SerializeField] private Transform viewRoot;
        private CharacterController controller;
        private Vector3 verticalVelocity;

        private void Awake() => controller = GetComponent<CharacterController>();

        public void Move(Vector2 input)
        {
            Vector3 forward = viewRoot ? viewRoot.forward : transform.forward;
            Vector3 right = viewRoot ? viewRoot.right : transform.right;
            forward.y = 0; right.y = 0;
            Vector3 move = (forward.normalized * input.y + right.normalized * input.x);
            if (move.sqrMagnitude > 1f) move.Normalize();
            controller.Move(move * moveSpeed * Time.deltaTime);
            if (controller.isGrounded && verticalVelocity.y < 0) verticalVelocity.y = -2f;
            verticalVelocity.y += gravity * Time.deltaTime;
            controller.Move(verticalVelocity * Time.deltaTime);
        }

        public void RequestDash(float distance, float duration)
        {
            StopAllCoroutines();
            StartCoroutine(DashRoutine(distance, duration));
        }

        private IEnumerator DashRoutine(float distance, float duration)
        {
            Vector3 direction = transform.forward;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                controller.Move(direction * (distance / duration) * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
