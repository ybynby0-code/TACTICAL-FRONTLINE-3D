using UnityEngine;

namespace TacticalFrontline.Core
{
    public sealed class TeamIdentity : MonoBehaviour
    {
        [SerializeField] private TeamId team = TeamId.None;
        public TeamId Team => team;
        public void SetTeam(TeamId value) => team = value;
        public bool IsEnemy(TeamIdentity other) =>
            other != null && team != TeamId.None && other.team != TeamId.None && team != other.team;
    }
}
