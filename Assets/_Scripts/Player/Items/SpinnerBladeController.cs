using UnityEngine;

public class SpinnerBladeController : MonoBehaviour
{
    [field: SerializeField] public float RotationSpeed { get; set; } = 180f;
    [field: SerializeField] public float RotationOffset { get; set; } = 0f;
    public Transform Target { get; set; }

    private void LateUpdate()
    {
        transform.position = Target.position;
        transform.rotation = Quaternion.Euler(0, 0, RotationSpeed * Time.timeSinceLevelLoad + RotationOffset);
    }
}
