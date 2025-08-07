using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHeath;
    [SerializeField] private bool isProjectile;
    [SerializeField] private UnityEvent onDeath;

    private float currentHealth;

    protected void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            onDeath?.Invoke();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<DamageDealer>(out var damageDealer))
        {
            if (isProjectile)
            {
                TakeDamage(damageDealer.DamageToProjectiles);
            }
            else
            {
                TakeDamage(damageDealer.DamageToCreatures);
            }
        }
    }
}

