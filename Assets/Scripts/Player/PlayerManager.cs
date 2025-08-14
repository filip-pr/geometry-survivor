using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Canvas healthBarCanvas;
    [SerializeField] private GameObject playerInstance;

    void Start()
    {
        if (playerInstance.TryGetComponent<Health>(out var playerHealth))
        {
            playerHealth.SetupHealthBar(healthBarCanvas);
        }
    }

}
