using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Script to manage health and taking damage for game objects.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class Health : MonoBehaviour
{
    [SerializeField] private float baseMaxHeath = 100f;
    [SerializeField] private float baseHealthRegen = 1f;

    public StatModifier MaxHealthModifier { get; set; }
    public StatModifier HealthRegenModifier { get; set; }

    public float MaxHeath => MaxHealthModifier == null ? baseMaxHeath : MaxHealthModifier.Modify(baseMaxHeath);
    public float HealthRegen => HealthRegenModifier == null ? baseHealthRegen : HealthRegenModifier.Modify(baseHealthRegen);

    [SerializeField] private float currentHealth = 0;

    [SerializeField] private GameObject healthBarPrefab;

    protected Slider healthBar;

    protected virtual void Start()
    {
        Heal(MaxHeath);
    }

    /// <summary>
    /// Set up the health bar UI for this object within the given canvas.
    /// </summary>
    public void SetupHealthBar(Canvas canvas)
    {
        healthBar = Instantiate(healthBarPrefab, canvas.transform).GetComponent<Slider>();
        healthBar.GetComponent<HealthBarFollow>().Target = transform;
        healthBar.maxValue = MaxHeath;
        UpdateHealthBar();
    }

    private void OnDestroy()
    {
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }
    }

    /// <summary>
    /// Handle death of the game object (health reaching zero).
    /// </summary>
    protected abstract void OnDeath();


    /// Updates the health bar UI value and hide it if health is full.
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

    /// <summary>
    /// Deal damage to the game object (possibly killing it).
    /// </summary>
    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath();
        }
        else if (currentHealth > MaxHeath)
        {
            currentHealth = MaxHeath;
        }
        UpdateHealthBar();

    }

    /// <summary>
    /// Heal the game object (possible killing it if health is negative).
    /// </summary>
    public void Heal(float health)
    {
        Damage(-health);
    }

    private void Update()
    {
        if (HealthRegen != 0)
        {
            Heal(HealthRegen * Time.deltaTime);
        }
    }
}

