using UnityEngine;
using UnityEngine.InputSystem;
using TacticalFrontline.Player;
using TacticalFrontline.Weapons;

public sealed class PlayerInputBridge : MonoBehaviour
{
    [SerializeField] private PlayerMotor motor;
    [SerializeField] private WeaponController weapon;
    [SerializeField] private Camera aimCamera;
    private InputAction move, fire, reload;

    private void OnEnable()
    {
        move = new InputAction("Move", InputActionType.Value);
        move.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w").With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a").With("Right", "<Keyboard>/d");
        fire = new InputAction("Fire", InputActionType.Button, "<Mouse>/leftButton");
        reload = new InputAction("Reload", InputActionType.Button, "<Keyboard>/r");
        move.Enable(); fire.Enable(); reload.Enable();
    }

    private void Update()
    {
        if (motor != null) motor.Move(move.ReadValue<Vector2>());
        if (fire.WasPressedThisFrame() && weapon != null)
            weapon.TryFire(aimCamera, gameObject);
        if (reload.WasPressedThisFrame() && weapon != null)
            weapon.BeginReload();
    }

    private void OnDisable()
    {
        move?.Disable(); fire?.Disable(); reload?.Disable();
    }
}
