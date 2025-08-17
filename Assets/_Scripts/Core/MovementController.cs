using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float knockbackResistance = 0f;
    [SerializeField] protected float knockbackDecayRate = 20f;

    private Vector2 knockbackVelocity = Vector2.zero;

    protected Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction, float force)
    {
        knockbackVelocity += direction.normalized * force * (1-knockbackResistance);
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
        if (knockbackVelocity.magnitude >= 0.01f)
        {
            knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, knockbackDecayRate * Time.fixedDeltaTime);
            rigidBody.linearVelocity = knockbackVelocity;
        }
        else
        {
            rigidBody.linearVelocity = moveSpeed * GetMovementDirection().normalized;
            UpdateRotation();
        }
    }
}
