using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHeath = 100f;
    [SerializeField] private float currentHealth;

    private DeathHandler deathHandler;

    private void Start()
    {
        currentHealth = maxHeath;
        deathHandler = GetComponent<DeathHandler>();
    }
    
    public void Heal(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHeath)
        {
            currentHealth = maxHeath;
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            deathHandler.HandleDeath();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<DamageDealer>(out var damageDealer))
        {
            Damage(damageDealer.EnemyDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<DamageDealer>(out var damageDealer))
        {
            Damage(damageDealer.ProjectileDamage);
        }
    }

}

