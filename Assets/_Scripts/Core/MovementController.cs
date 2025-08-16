using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float knockbackResistance = 0.5f;

    protected Vector2 knockbackVelocity = Vector2.zero;
    protected Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction, float force)
    {
        knockbackVelocity += direction.normalized * force;
    }

    protected abstract Vector2 GetMovementDirection();

    protected virtual void UpdateRotation()
    {
        if (rigidBody.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rigidBody.linearVelocityY, rigidBody.linearVelocityX) * Mathf.Rad2Deg - 90f;
            rigidBody.MoveRotation(angle);
        }
    }

    private void FixedUpdate()
    {
        knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, knockbackResistance * Time.fixedDeltaTime);
        rigidBody.linearVelocity = moveSpeed * GetMovementDirection().normalized + knockbackVelocity;
        UpdateRotation();
    }
}
