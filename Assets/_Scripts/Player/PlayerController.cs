using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovementController
{
    [SerializeField] private float moveSpeed;
    private Vector2 moveInput = Vector2.zero;

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    protected override void UpdateVelocity()
    {
        rigidBody.linearVelocity = moveInput * moveSpeed;
    }

    protected override void UpdateRotation()
    {
        if (moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90f;
            rigidBody.MoveRotation(angle);
        }
    }
}
