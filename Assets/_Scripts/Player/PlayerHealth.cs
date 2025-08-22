using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerHealth : Health
{
    protected override void Start()
    {
        PlayerStats playerStats = GetComponent<PlayerStats>();

        HealthRegenModifier = playerStats.HealthRegenModifier;
        MaxHealthModifier = playerStats.MaxHealthModifier;
        base.Start();
    }

    protected override void OnDeath()
    {
        Debug.Log("Player has died.");
    }
}
