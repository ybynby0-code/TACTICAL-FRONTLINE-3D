using UnityEngine;

namespace TacticalFrontline.Heroes
{
    public abstract class AbilityDefinition : ScriptableObject
    {
        public string abilityId;
        public string displayName;
        [Min(0.1f)] public float cooldown = 8f;
        [TextArea] public string description;
        public abstract void Activate(GameObject user);
    }
}
