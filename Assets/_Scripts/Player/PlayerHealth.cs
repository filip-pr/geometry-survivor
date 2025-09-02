using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerHealth : Health
{
    private GameManager gameManager;

    protected override void Start()
    {
        PlayerStats playerStats = GetComponent<PlayerStats>();

        HealthRegenModifier = playerStats.HealthRegenModifier;
        MaxHealthModifier = playerStats.MaxHealthModifier;
        base.Start();
    }

    public void SetupDeath(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    protected override void OnDeath()
    {
        gameManager.EndGame();
    }
}
