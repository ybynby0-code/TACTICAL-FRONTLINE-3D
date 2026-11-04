using UnityEngine;

namespace TacticalFrontline.Heroes
{
    public sealed class HeroAbilityRuntime : MonoBehaviour
    {
        [SerializeField] private HeroDefinition hero;
        private float[] readyAt;

        private void Awake()
        {
            int count = hero != null && hero.abilities != null ? hero.abilities.Length : 0;
            readyAt = new float[count];
        }

        public bool TryActivate(int slot)
        {
            if (hero == null || hero.abilities == null ||
                slot < 0 || slot >= hero.abilities.Length) return false;

            AbilityDefinition ability = hero.abilities[slot];
            if (ability == null || Time.time < readyAt[slot]) return false;
            ability.Activate(gameObject);
            readyAt[slot] = Time.time + ability.cooldown;
            return true;
        }

        public float GetCooldownRemaining(int slot)
        {
            if (readyAt == null || slot < 0 || slot >= readyAt.Length) return 0f;
            return Mathf.Max(0f, readyAt[slot] - Time.time);
        }
    }
}
