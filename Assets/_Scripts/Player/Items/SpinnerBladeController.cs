using UnityEngine;

public class SpinnerBladeController : MonoBehaviour
{
    [field: SerializeField] public float RotationSpeed { get; set; } = 180f;
    [field: SerializeField] public float RotationOffset { get; set; } = 0f;
    public Transform Target { get; set; }

    public StatModifier AttackSpeedModifier { get; set; }

    private void LateUpdate()
    {
        transform.position = Target.position;
        transform.rotation = Quaternion.Euler(0, 0, AttackSpeedModifier.Modify(RotationSpeed) * Time.timeSinceLevelLoad + RotationOffset);
    }
}
