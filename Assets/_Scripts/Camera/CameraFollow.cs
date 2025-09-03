using UnityEngine;

/// <summary>
/// Script handling camera target follow movement with smoothing.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.15f;
    [field: SerializeField] public Transform Target { get; set; }

    private Vector2 cameraVelocity = Vector2.zero;

    private void LateUpdate()
    {
        if (Target == null) return;
        transform.position = (Vector3)Vector2.SmoothDamp(transform.position, Target.position, ref cameraVelocity, smoothTime) + new Vector3(0, 0, -1);
    }
}
