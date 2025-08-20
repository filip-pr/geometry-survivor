using UnityEngine;

public class EnemyExperienceDropDeathHandler : DeathHandler
{
    [SerializeField] private GameObject experiencePrefab;

    public override void HandleDeath()
    {
        if (experiencePrefab != null)
        {
            Instantiate(experiencePrefab, transform.position, Quaternion.identity, transform);
        }
        base.HandleDeath();
    }

}
