using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Camera target.")]
    [SerializeField] private Transform target;

    [Tooltip("Camera offset.")]
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -15f);

    [Tooltip("Camera smoothing strength.")]
    [SerializeField] private float smoothingStrength = 0.15f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        transform.position = Vector3.SmoothDamp(transform.position, target.position + cameraOffset, ref velocity, smoothingStrength);
    }
}