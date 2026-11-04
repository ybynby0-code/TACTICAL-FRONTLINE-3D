using UnityEngine;
using TacticalFrontline.Heroes;
using TacticalFrontline.Player;

[CreateAssetMenu(menuName = "Tactical Frontline/Abilities/Dash")]
public sealed class DashAbility : AbilityDefinition
{
    [SerializeField] private float dashDistance = 7f;
    [SerializeField] private float dashDuration = 0.18f;

    public override void Activate(GameObject user)
    {
        if (user == null) return;
        var motor = user.GetComponent<PlayerMotor>();
        if (motor != null) motor.RequestDash(dashDistance, dashDuration);
    }
}
