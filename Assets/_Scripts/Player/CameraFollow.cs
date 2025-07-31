using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -15f);

    [SerializeField] private float smoothingStrength = 0.15f;

    private Vector3 velocity;

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + cameraOffset, ref velocity, smoothingStrength);
    }
}