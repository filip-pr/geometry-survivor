using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform Target { get; set; }
    [SerializeField] private Vector3 offset = new(0, -0.7f, 0);

    private void LateUpdate()
    {
        transform.position = Target.position + offset;
    }
}
