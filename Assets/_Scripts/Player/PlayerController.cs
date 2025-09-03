using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script to manage player movement.
/// </summary>
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MovementController
{
    private Vector2 moveInput = Vector2.zero;

    public PlayerInput PlayerInput { get; set; }

    protected override void Start()
    {
        MovementSpeedModifier = gameObject.GetComponent<PlayerStats>().MovementSpeedModifier;
        base.Start();
    }

    protected override Vector2 GetMovementDirection()
    {
        return PlayerInput.actions["Move"].ReadValue<Vector2>();
    }
}
