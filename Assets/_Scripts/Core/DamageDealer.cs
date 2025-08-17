using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float baseDamage = 0.2f;

    public float DamageMultiplier { get; set; } = 1f;
    public float Damage => baseDamage * DamageMultiplier;

    private void HandleCollision(GameObject other)
    {
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
