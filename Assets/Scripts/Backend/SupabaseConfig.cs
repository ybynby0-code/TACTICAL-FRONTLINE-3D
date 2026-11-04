using UnityEngine;

namespace TacticalFrontline.Backend
{
    [CreateAssetMenu(menuName = "Tactical Frontline/Supabase Config")]
    public sealed class SupabaseConfig : ScriptableObject
    {
        [Tooltip("Project URL only. Never put a service_role key in a game client.")]
        public string projectUrl;
        [Tooltip("Use the publishable/anon client key. Protect privileged operations server-side.")]
        public string publishableKey;
    }
}
