using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHeath = 100f;
    [SerializeField] private float currentHealth = 0;

    [SerializeField] private GameObject healthBarPrefab;
    private Slider healthBar;
    
    private DeathHandler deathHandler;

    private void Awake()
    {
        currentHealth = maxHeath;
        healthBar = Instantiate(healthBarPrefab, transform).GetComponent<Slider>();
        deathHandler = GetComponent<DeathHandler>();
        Heal(maxHeath);
    }

    public void SetupHealthBar(Canvas HealthBarCanvas)
    {
        if (healthBar == null) return;
        healthBar.transform.SetParent(HealthBarCanvas.transform, false);
        healthBar.maxValue = maxHeath;
        healthBar.GetComponent<HealthBarFollow>().Target = transform;
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null) return;
        if (currentHealth == maxHeath)
        {
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
        }
        healthBar.value = currentHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            deathHandler.HandleDeath();
        }
        else if (currentHealth > maxHeath)
        {
            currentHealth = maxHeath;
        }
        UpdateHealthBar();

    }
    public void Heal(float health)
    {
        Damage(-health);
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

