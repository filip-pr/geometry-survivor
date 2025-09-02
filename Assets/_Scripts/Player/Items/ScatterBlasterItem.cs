using UnityEngine;

public class ScatterBlasterItem : ProjectileWeaponItem
{
    public override string ItemName => "Scatter Blaster";

    public override int MaxLevel => 5;

    [SerializeField] private int numProjectiles = 7;
    [SerializeField] private float spreadAngle = 30f;

    protected override void Fire()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * transform.up;
            GameObject projectile = SpawnProjectile();
            projectile.GetComponent<ProjectileController>().Setup(transform.position, direction);
        }
    }

    protected override void OnLevelUp()
    {
        switch (Level)
        {
            case 1:
                break;
            case 2:
                damageModifier.IncreaseMultiplier(0.125f);
                attackSpeedModifier.IncreaseMultiplier(0.125f);
                numProjectiles += 2;
                break;
            case 3:
                damageModifier.IncreaseMultiplier(0.25f);
                attackSpeedModifier.IncreaseMultiplier(0.25f);
                break;
            case 4:
                damageModifier.IncreaseMultiplier(0.5f);
                attackSpeedModifier.IncreaseMultiplier(0.5f);
                numProjectiles += 2;
                break;
            case 5:
                damageModifier.IncreaseMultiplier(1f);
                attackSpeedModifier.IncreaseMultiplier(1f);
                break;
        }
    }
}

