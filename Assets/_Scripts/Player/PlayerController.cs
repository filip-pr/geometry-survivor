using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MovementController
{
    private Vector2 moveInput = Vector2.zero;

    protected override void Start()
    {
        movementSpeedModifier = gameObject.GetComponent<PlayerStats>().MovementSpeedModifier;
        base.Start();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    protected override Vector2 GetMovementDirection()
    {
        return moveInput;
    }
}
