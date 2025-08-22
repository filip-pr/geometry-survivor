using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerExperienceMagnet : MonoBehaviour
{
    [SerializeField] private PlayerLevel playerLevel;

    [SerializeField] private float baseRange = 2f;

    [SerializeField] public StatModifier RangeModifier { get; set; }

    public float Range => RangeModifier == null ? baseRange : RangeModifier.Modify(baseRange);
    private float MagnetPullSpeed => Range * 5f;

    private CircleCollider2D magnetCollider;
    private HashSet<GameObject> experienceInRange = new();

    private void Start()
    {
        magnetCollider = GetComponent<CircleCollider2D>();

        PlayerStats playerStats = GetComponentInParent<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerExperienceMagnet could not find PlayerStats component in parent.");
        }
        else
        {
            RangeModifier = playerStats.ExperienceMagnetRangeModifier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyExperience>(out var enemyXP))
        {
            experienceInRange.Add(collision.gameObject);

        }
    }

    private void FixedUpdate()
    {
        magnetCollider.radius = Range;
    }

    private void LateUpdate()
    {
        foreach (var experience in experienceInRange)
        {
            if (experience == null) continue;
            EnemyExperience enemyXP = experience.GetComponent<EnemyExperience>();
            enemyXP.transform.position = Vector2.MoveTowards(
                enemyXP.transform.position,
                transform.position,
                MagnetPullSpeed * Time.deltaTime
            );
            if (Vector2.Distance(enemyXP.transform.position, transform.position) < 0.1f)
            {
                playerLevel.GainExperience(enemyXP.Experience);
                Destroy(experience);
            }
        }
        experienceInRange.RemoveWhere(x => x == null);
    }
}
