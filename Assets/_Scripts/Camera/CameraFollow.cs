using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;

    private Vector3 cameraVelocity;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, smoothTime);
    }
}
