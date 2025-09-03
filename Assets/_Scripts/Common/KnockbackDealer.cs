using UnityEngine;

/// <summary>
/// Script to manage knockback dealing for game objects.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class KnockbackDealer : MonoBehaviour
{
    [SerializeField] private float baseKnockback = 20f;

    public StatModifier KnockbackModifier { get; set; }

    public float Knockback => KnockbackModifier == null ? baseKnockback : KnockbackModifier.Modify(baseKnockback);

    /// <summary>
    /// Handle collision with other game objects, dealing knockback if they have a MovementController component and don't have the same tag.
    /// </summary>
    private void HandleCollision(GameObject other)
    {
        if (gameObject.tag == other.tag)
        {
            return;
        }
        if (other.TryGetComponent<MovementController>(out var movementController))
        {
            movementController.Push(other.transform.position - transform.position, Knockback);
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
