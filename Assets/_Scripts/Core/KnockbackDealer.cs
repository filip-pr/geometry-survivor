using UnityEngine;

public class KnockbackDealer : MonoBehaviour
{
    [SerializeField] private float baseKnockback = 20f;

    public float KnockbackMultiplier { get; set; } = 1f;

    public float Knockback => baseKnockback * KnockbackMultiplier;

    private void HandleCollision(GameObject other)
    {
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
