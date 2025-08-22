using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovementController : MonoBehaviour
{
    [SerializeField] protected float baseMovementSpeed = 5f;
    [SerializeField] protected float baseKnockbackResistance = 0f;
    [SerializeField] protected float baseKnockbackDecayRate = 20f;

    public StatModifier movementSpeedModifier { get; set; }
    public StatModifier knockbackResistanceModifier { get; set; }
    public StatModifier knockbackDecayRateModifier { get; set; }

    public float MovementSpeed => movementSpeedModifier == null ? baseMovementSpeed : movementSpeedModifier.Modify(baseMovementSpeed);
    public float knockbackResistance => knockbackResistanceModifier == null ? baseKnockbackResistance : knockbackResistanceModifier.Modify(baseKnockbackResistance);
    public float knockbackDecayRate => knockbackDecayRateModifier == null ? baseKnockbackDecayRate : knockbackDecayRateModifier.Modify(baseKnockbackDecayRate);


    private Vector2 knockbackVelocity = Vector2.zero;

    protected Rigidbody2D rigidBody;

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction, float force)
    {
        knockbackVelocity += direction.normalized * force * Mathf.Max(1-knockbackResistance, 0);
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
            rigidBody.linearVelocity = MovementSpeed * GetMovementDirection().normalized;
            UpdateRotation();
        }
    }
}
