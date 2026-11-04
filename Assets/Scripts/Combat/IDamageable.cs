using UnityEngine;

namespace TacticalFrontline.Combat
{
    public interface IDamageable
    {
        void TakeDamage(float amount, GameObject instigator);
    }
}
