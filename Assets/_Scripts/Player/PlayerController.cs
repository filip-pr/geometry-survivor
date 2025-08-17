using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovementController
{
    private Vector2 moveInput = Vector2.zero;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    public float KnockbackResistance
    {
        get => knockbackResistance;
        set => knockbackResistance = value;
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
