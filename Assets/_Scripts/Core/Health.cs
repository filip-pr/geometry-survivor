using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxHeath = 100f;
    [SerializeField] private float currentHealth = 0;

    [SerializeField] private GameObject healthBarPrefab;
    private Slider healthBar;
    
    private DeathHandler deathHandler;

    private void Awake()
    {
        currentHealth = MaxHeath;
        healthBar = Instantiate(healthBarPrefab, transform).GetComponent<Slider>();
        deathHandler = GetComponent<DeathHandler>();
        Heal(MaxHeath);
    }

    public void SetupHealthBar(Canvas HealthBarCanvas)
    {
        if (healthBar == null) return;
        healthBar.transform.SetParent(HealthBarCanvas.transform, false);
        healthBar.maxValue = MaxHeath;
        healthBar.GetComponent<HealthBarFollow>().Target = transform;
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null) return;
        if (currentHealth == MaxHeath)
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
        else if (currentHealth > MaxHeath)
        {
            currentHealth = MaxHeath;
        }
        UpdateHealthBar();

    }
    public void Heal(float health)
    {
        Damage(-health);
    }

}

