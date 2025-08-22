using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    [SerializeField] private GameObject experiencePrefab;

    protected override void OnDeath()
    {
        if (experiencePrefab != null)
        {
            Instantiate(experiencePrefab, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }

}
