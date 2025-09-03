using UnityEngine;


/// <summary>
/// Abstract base class to manage movement, rotation and knockback for game objects.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovementController : MonoBehaviour
{
    [SerializeField] protected float baseMovementSpeed = 5f;
    [SerializeField] protected float baseKnockbackResistance = 0f;
    [SerializeField] protected float baseKnockbackDecayRate = 20f;

    public StatModifier MovementSpeedModifier { get; set; }
    public StatModifier KnockbackResistanceModifier { get; set; }
    public StatModifier KnockbackDecayRateModifier { get; set; }
    public bool IsGhosting { get; set; } = false;
    public float MovementSpeed => MovementSpeedModifier == null ? baseMovementSpeed : MovementSpeedModifier.Modify(baseMovementSpeed);
    public float knockbackResistance => KnockbackResistanceModifier == null ? baseKnockbackResistance : KnockbackResistanceModifier.Modify(baseKnockbackResistance);
    public float knockbackDecayRate => KnockbackDecayRateModifier == null ? baseKnockbackDecayRate : KnockbackDecayRateModifier.Modify(baseKnockbackDecayRate);


    private Vector2 knockbackVelocity = Vector2.zero;

    protected Rigidbody2D rigidBody;

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Deal knockback to the object in a given direction with a certain force. 
    /// </summary>
    public void Push(Vector2 direction, float force)
    {
        knockbackVelocity += direction.normalized * force * Mathf.Max(1-knockbackResistance, 0);
    }

    /// <summary>
    /// Get the desired movement direction of the object.
    /// </summary>
    protected abstract Vector2 GetMovementDirection();

    /// <summary>
    /// Update the rotation of the object.
    /// </summary>
    protected virtual void UpdateRotation()
    {
        if (rigidBody.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rigidBody.linearVelocityY, rigidBody.linearVelocityX) * Mathf.Rad2Deg - 90f;
            rigidBody.MoveRotation(angle);
        }
    }

    /// <summary>
    /// Increase the object's velocity by a vector or move it directly if it's ghosting.
    /// </summary>
    private void UpdateVelocity(Vector2 amount)
    {
        if (IsGhosting)
        {
            transform.position += (Vector3)(amount*Time.deltaTime);
        }
        else
        {
            rigidBody.linearVelocity = amount;
        }
    }

    private void FixedUpdate()
    {
        if (knockbackVelocity.magnitude >= 0.01f)
        {
            knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, knockbackDecayRate * Time.fixedDeltaTime);
            UpdateVelocity(knockbackVelocity);
        }
        else
        {
            UpdateVelocity(MovementSpeed * GetMovementDirection().normalized);
            UpdateRotation();
        }
    }
}
