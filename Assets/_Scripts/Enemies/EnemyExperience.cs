using UnityEngine;

/// <summary>
/// Script to manage enemy experience drops.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class EnemyExperience : MonoBehaviour
{
    [SerializeField] private float lifeDuration = 60f;
    [field: SerializeField] public float Experience { get; private set; }

    private float lifeTimer = 0f;

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeDuration)
        {
            Destroy(gameObject);
        }
    }
}
