using UnityEngine;
using System.Collections.Generic;

public class PlayerExperienceMagnet : MonoBehaviour
{
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] private float magnetRange = 2f;

    private HashSet<GameObject> experienceInRange = new();
    private float MagnetRange
    {
        get { return magnetRange; }
        set
        {
            if (TryGetComponent<CircleCollider2D>(out var collider))
            {
                collider.radius = value;
            }
            magnetRange = value;
        }
    }
    private float MagnetPullSpeed => MagnetRange * 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyExperience>(out var enemyXP))
        {
            experienceInRange.Add(collision.gameObject);

        }
    }

    private void OnValidate()
    {
        MagnetRange = magnetRange;
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
