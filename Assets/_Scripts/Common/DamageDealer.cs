using UnityEngine;


/// <summary>
/// Script to deal damage to other game objects upon collision.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float baseDamage = 0.2f;

    public StatModifier DamageModifier { get; set; }

    public float Damage => DamageModifier == null ? baseDamage : DamageModifier.Modify(baseDamage);

    protected virtual void Start()
    {
        if (DamageModifier == null)
        {
            DamageModifier = new StatModifier();
        }
    }

    /// <summary>
    /// Handle collision with other game objects, dealing damage if they have a Health component and don't have the same tag.
    /// </summary>
    private void HandleCollision(GameObject other)
    {
        if (gameObject.tag == other.tag)
        {
            return;
        }
        if (other.TryGetComponent<Health>(out var health))
        {
            health.Damage(Damage);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        HandleCollision(collider.gameObject);
    }


}
