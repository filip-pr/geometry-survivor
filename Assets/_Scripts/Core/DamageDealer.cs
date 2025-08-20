using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float baseDamage = 0.2f;
    public DamageModifier DamageModifier { get; set; }
    public float Damage => DamageModifier == null ? baseDamage : DamageModifier.Modify(baseDamage);

    private void Start()
    {
        if (DamageModifier == null)
        {
            DamageModifier = new DamageModifier();
        }
    }

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
