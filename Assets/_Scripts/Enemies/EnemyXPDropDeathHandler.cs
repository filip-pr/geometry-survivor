using UnityEngine;

public class EnemyXPDropDeathHandler : DeathHandler
{
    [SerializeField] private GameObject experiencePrefab;

    public override void HandleDeath()
    {
        if (experiencePrefab != null)
        {
            Instantiate(experiencePrefab, transform.position, Quaternion.identity);
        }
        base.HandleDeath();
    }

}
