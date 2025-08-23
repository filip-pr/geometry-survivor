using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MovementController
{
    private Vector2 moveInput = Vector2.zero;

    public PlayerInput PlayerInput { get; set; }

    protected override void Start()
    {
        movementSpeedModifier = gameObject.GetComponent<PlayerStats>().MovementSpeedModifier;
        base.Start();
    }

    protected override Vector2 GetMovementDirection()
    {
        return PlayerInput.actions["Move"].ReadValue<Vector2>();
    }
}
