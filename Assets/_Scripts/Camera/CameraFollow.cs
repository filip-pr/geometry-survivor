using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.15f;
    [field: SerializeField] public Transform Target { get; set; }

    private Vector3 cameraVelocity = Vector3.zero;

    private void LateUpdate()
    {
        if (Target == null) return;
        transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref cameraVelocity, smoothTime) + new Vector3(0, 0, -1);
    }
}
