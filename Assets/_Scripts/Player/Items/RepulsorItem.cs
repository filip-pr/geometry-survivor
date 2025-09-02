using UnityEngine;

public class RepulsorItem : ProjectileWeaponItem
{
    public override string ItemName => "Repulsor";

    public override int MaxLevel => 5;

    protected override void Fire()
    {
        GameObject projectile = SpawnProjectile();
        projectile.GetComponent<RepulsorProjectileController>().Target = transform;
    }

    protected override void OnLevelUp()
    {
        switch (Level)
        {
            case 1:
                break;
            case 2:
                knockBackModifier.IncreaseMultiplier(0.125f);
                attackSpeedModifier.IncreaseMultiplier(0.05f);
                break;
            case 3:
                knockBackModifier.IncreaseMultiplier(0.25f);
                attackSpeedModifier.IncreaseMultiplier(0.125f);
                break;
            case 4:
                knockBackModifier.IncreaseMultiplier(0.5f);
                attackSpeedModifier.IncreaseMultiplier(0.25f);
                break;
            case 5:
                knockBackModifier.IncreaseMultiplier(1f);
                attackSpeedModifier.IncreaseMultiplier(0.5f);
                break;
        }
    }
}
