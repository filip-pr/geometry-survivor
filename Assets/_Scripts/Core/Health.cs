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

    private void HandleCollision(GameObject other)
    {
        if (other.TryGetComponent<DamageDealer>(out var damageDealer))
        {
            Damage(damageDealer.Damage);
        }
        if (gameObject.TryGetComponent<MovementController>(out var movementController))
        {
            movementController.Push(other.transform.position - transform.position, damageDealer.Knockback);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        HandleCollision(transform.parent.gameObject);
    }

}

