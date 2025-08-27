using UnityEngine;

public class EnemyController : MovementController
{
    [field: SerializeField] public Transform Target { get; set; }

    [SerializeField] private float ghostTimeThreshold = 60f;
    [SerializeField] private float speedIncreaseThreshold = 120f;
    [SerializeField] private float speedIncreaseAmmount = 0.05f;

    private float timeAlive = 0f;

    protected override void UpdateRotation()
    {
        if (rigidBody.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rigidBody.linearVelocity.y, rigidBody.linearVelocity.x) * Mathf.Rad2Deg;
            rigidBody.rotation = angle;
        }
    }

    protected override Vector2 GetMovementDirection()
    {
        if (Target == null) return Vector2.zero;
        return (rigidBody.position - (Vector2)Target.position);
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > ghostTimeThreshold)
        {
            IsGhosting = true;
        }
        if (timeAlive > speedIncreaseThreshold)
        {
            MovementSpeedModifier.IncreaseFlat(speedIncreaseAmmount * Time.deltaTime);
        }
    }
}
