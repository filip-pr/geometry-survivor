using UnityEngine;

public class EnemyController : MovementController
{

    [SerializeField] private float desiredDistanceMin = 0f;
    [SerializeField] private float desiredDistanceMax = 0.1f;
    [field: SerializeField] public Transform Target { get; set; }

    private float TargetDistance
    {
        get
        {
            if (Target == null) return 0f;
            return Vector2.Distance(rigidBody.position, Target.position);
        }
    }

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

        if (TargetDistance < desiredDistanceMin)
        {
            return (rigidBody.position - (Vector2)Target.position);
        }
        else if (TargetDistance > desiredDistanceMax)
        {
            return ((Vector2)Target.position - rigidBody.position);
        }
        else
        {
            return Vector2.zero;
        }
    }
}
