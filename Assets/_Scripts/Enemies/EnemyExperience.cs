using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyExperience : MonoBehaviour
{
    [field: SerializeField] public float Experience { get; private set; }
}
