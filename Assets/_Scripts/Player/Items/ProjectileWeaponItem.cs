using UnityEngine;

public abstract class ProjectileWeaponItem : PlayerItem
{

    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] protected float baseFireRate = 1f;

    protected float lastFireTime = 0f;

    protected bool ShouldFire => Time.time >= lastFireTime + (1f / attackSpeedModifier.Modify(baseFireRate));

    protected StatModifier damageModifier;
    protected StatModifier knockBackModifier;
    protected StatModifier attackSpeedModifier;

    protected GameObject SpawnProjectile() {
        GameObject projectile = Instantiate(ProjectilePrefab, ProjectileParent);
        if (projectile.TryGetComponent<DamageDealer>(out var damageDealer))
        {
            damageDealer.DamageModifier = damageModifier;
        }
        if (projectile.TryGetComponent<KnockbackDealer>(out var knockbackDealer))
        {
            knockbackDealer.KnockbackModifier = knockBackModifier;
        }
        return projectile;
    }

    public override void SetupModifiers(PlayerStats playerStats)
    {
        damageModifier = new StatModifier(playerStats.DamageModifier);
        knockBackModifier = new StatModifier(playerStats.KnockbackModifier);
        attackSpeedModifier = new StatModifier();
    }

    protected abstract void Fire();

    private void Update()
    {
        if (ShouldFire)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }
}
