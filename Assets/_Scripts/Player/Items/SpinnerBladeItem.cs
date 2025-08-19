using UnityEngine;
using System.Collections.Generic;

public class SpinnerBladeItem : PlayerItem
{
    public override int MaxLevel => 5;

    [SerializeField] private GameObject spinnerBladePrefab;

    [SerializeField] private List<GameObject> spinnerBlades = new();

    protected override void OnLevelUp()
    {
        foreach (var blade in spinnerBlades)
        {
            Destroy(blade);
        }
        spinnerBlades.Clear();
        switch (Level)
        {
            case 1:
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                break;
            case 2:
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                break;
            case 3:
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                spinnerBlades[1].GetComponent<SpinnerBladeController>().RotationOffset = 180f;
                break;
            case 4:
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                spinnerBlades[1].GetComponent<SpinnerBladeController>().RotationOffset = 180f;
                break;
            case 5:
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                spinnerBlades[1].GetComponent<SpinnerBladeController>().RotationOffset = 90f;
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                spinnerBlades[2].GetComponent<SpinnerBladeController>().RotationOffset = 180f;
                spinnerBlades.Add(Instantiate(spinnerBladePrefab, transform));
                spinnerBlades[3].GetComponent<SpinnerBladeController>().RotationOffset = 270f;
                break;
        }
    }
}
