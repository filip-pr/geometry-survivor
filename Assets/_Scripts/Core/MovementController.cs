using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    protected Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    protected abstract void UpdateVelocity();

    protected abstract void UpdateRotation();

    void FixedUpdate()
    {
        UpdateVelocity();
        UpdateRotation();
    }
}
