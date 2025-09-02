using UnityEngine;

public class SpinnerBladeItem : PlayerItem
{
    public override int MaxLevel => 5;

    [SerializeField] private GameObject spinnerBladePrefab;

    private StatModifier damageModifier;
    private StatModifier knockBackModifier;
    private StatModifier attackSpeedModifier;

    public override string ItemName => "Spinner Blade";
    private void AddBlade(float offset)
    {
        GameObject newBlade = Instantiate(spinnerBladePrefab, ProjectileParent);
        newBlade.GetComponent<SpinnerBladeController>().RotationOffset = offset;
        newBlade.GetComponent<SpinnerBladeController>().Target = transform;
        newBlade.GetComponent<SpinnerBladeController>().AttackSpeedModifier = attackSpeedModifier;
        newBlade.GetComponent<DamageDealer>().DamageModifier = damageModifier;
        newBlade.GetComponent<KnockbackDealer>().KnockbackModifier = knockBackModifier;
    }

    protected override void OnLevelUp()
    {
        switch (Level)
        {
            case 1:
                AddBlade(0f);
                break;
            case 2:
                attackSpeedModifier.IncreaseMultiplier(0.125f);
                knockBackModifier.IncreaseMultiplier(0.25f);
                break;
            case 3:
                AddBlade(180f);
                break;
            case 4:
                attackSpeedModifier.IncreaseMultiplier(0.25f);
                damageModifier.IncreaseMultiplier(0.5f);
                break;
            case 5:
                AddBlade(90f);
                AddBlade(270f);
                break;
        }
    }

    public override void SetupModifiers(PlayerStats playerStats)
    {
        damageModifier = new StatModifier(playerStats.DamageModifier);
        knockBackModifier = new StatModifier(playerStats.KnockbackModifier);
        attackSpeedModifier = new StatModifier();
    }
}
