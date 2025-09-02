using UnityEngine;

public class BlasterItem : ProjectileWeaponItem
{
    public override string ItemName => "Blaster";
    public override int MaxLevel => 5;

    protected override void Fire()
    {
        GameObject projectile = SpawnProjectile();
        projectile.GetComponent<ProjectileController>().Setup(transform.position, transform.up);
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
                break;
            case 3:
                damageModifier.IncreaseMultiplier(0.25f);
                attackSpeedModifier.IncreaseMultiplier(0.25f);
                break;
            case 4:
                damageModifier.IncreaseMultiplier(0.5f);
                attackSpeedModifier.IncreaseMultiplier(0.5f);
                break;
            case 5:
                damageModifier.IncreaseMultiplier(1f);
                attackSpeedModifier.IncreaseMultiplier(1f);
                break;
        }
    }
}

