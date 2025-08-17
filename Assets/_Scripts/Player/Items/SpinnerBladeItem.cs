using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpinnerBladeItem : PlayerItem
{
    public override int MaxLevel => 1;

    [SerializeField] private GameObject spinnerBladePrefab;

    [SerializeField] private GameObject[] spinnerBlades;

    protected override void OnLevelUp()
    {
        foreach (var blade in spinnerBlades)
        {
            Destroy(blade);
        }
        switch (Level)
        {
            case 1:
                Instantiate(spinnerBladePrefab, transform.position, Quaternion.identity, transform);
                break;
            
        }
    }
}
