using UnityEngine;
using TacticalFrontline.Combat;

[RequireComponent(typeof(HealthComponent))]
public sealed class PracticeTarget : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 2.5f;
    private HealthComponent health;
    private Renderer[] renderers;
    private Collider[] colliders;
    private Vector3 startPosition;

    private void Awake()
    {
        health = GetComponent<HealthComponent>();
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
        startPosition = transform.position;
        health.Died += OnDied;
    }

    private void OnDestroy()
    {
        if (health != null) health.Died -= OnDied;
    }

    private void OnDied(GameObject attacker)
    {
        foreach (var r in renderers) r.enabled = false;
        foreach (var c in colliders) c.enabled = false;
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        transform.position = startPosition;
        health.ResetHealth();
        foreach (var r in renderers) r.enabled = true;
        foreach (var c in colliders) c.enabled = true;
    }
}
