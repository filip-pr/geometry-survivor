using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public virtual void HandleDeath()
    {
        Destroy(gameObject);
    }
}
