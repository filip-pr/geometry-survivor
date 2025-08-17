using UnityEngine;

public class SpinnerBladeController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;

    private void LateUpdate()
    {
        transform.position = transform.parent.position;
        transform.rotation = Quaternion.Euler(0, 0, Time.time * rotationSpeed);
    }
}
