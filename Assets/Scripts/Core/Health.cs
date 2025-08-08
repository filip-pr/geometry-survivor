using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHeath = 100f;
    [SerializeField] private bool isProjectile = false;
    [SerializeField] private UnityEvent onDeath;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHeath;
    }
    
    protected void Heal(float health)
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
            onDeath?.Invoke();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<DamageDealer>(out var damageDealer))
        {
            if (isProjectile)
            {
                Damage(damageDealer.DamageToProjectiles);
            }
            else
            {
                Damage(damageDealer.DamageToCreatures);
            }
        }
    }
}

