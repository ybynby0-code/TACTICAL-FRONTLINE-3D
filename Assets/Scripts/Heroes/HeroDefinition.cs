using UnityEngine;

namespace TacticalFrontline.Heroes
{
    [CreateAssetMenu(menuName = "Tactical Frontline/Hero Definition")]
    public class HeroDefinition : ScriptableObject
    {
        public string heroId;
        public string displayName;
        [TextArea] public string roleDescription;
        public float maxHealth = 200f;
        public float moveSpeed = 5.5f;
        public GameObject characterPrefab;
        public AbilityDefinition[] abilities;
    }
}
