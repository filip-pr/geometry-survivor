using UnityEngine;
using UnityEngine.InputSystem;

public class BallCannonItem : ProjectileWeaponItem
{
    public override string ItemName => "Ball Cannon";
    private const float projectileSpawnOffset = 0.5f;

    public override int MaxLevel => 5;

    protected override void OnLevelUp()
    {
        switch (Level)
        {
            case 1:
                break;
            case 2:
                damageModifier.IncreaseMultiplier(0.125f);
                knockBackModifier.IncreaseMultiplier(0.125f);
                break;
            case 3:
                damageModifier.IncreaseMultiplier(0.25f);
                knockBackModifier.IncreaseMultiplier(0.25f);
                break;
            case 4:
                damageModifier.IncreaseMultiplier(0.5f);
                knockBackModifier.IncreaseMultiplier(0.5f);
                break;
            case 5:
                damageModifier.IncreaseMultiplier(1f);
                knockBackModifier.IncreaseMultiplier(1f);
                break;
        }
    }

    protected override void Fire()
    {
        GameObject projectile = SpawnProjectile();
        projectile.GetComponent<ProjectileController>().Setup(transform.position + transform.up * projectileSpawnOffset, transform.up);
    }
}
