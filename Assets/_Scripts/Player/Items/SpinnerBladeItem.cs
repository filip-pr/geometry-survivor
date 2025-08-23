using UnityEngine;
using System.Collections.Generic;

public class SpinnerBladeItem : PlayerItem
{
    public override int MaxLevel => 5;

    [SerializeField] private GameObject spinnerBladePrefab;


    private void AddBlade(float offset)
    {
        GameObject newBlade = Instantiate(spinnerBladePrefab, ProjectileParent);
        newBlade.GetComponent<SpinnerBladeController>().RotationOffset = offset;
        newBlade.GetComponent<SpinnerBladeController>().Target = transform;
    }

    protected override void OnLevelUp()
    {
        switch (Level)
        {
            case 1:
                AddBlade(0f);
                break;
            case 2:
                break;
            case 3:
                AddBlade(180f);
                break;
            case 4:
                break;
            case 5:
                AddBlade(90f);
                AddBlade(270f);
                break;
        }
    }
}
