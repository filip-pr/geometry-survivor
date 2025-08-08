using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public void HandleDeath()
    {
        Destroy(gameObject);
    }
}
