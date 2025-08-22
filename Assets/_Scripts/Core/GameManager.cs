using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas menuUICanvas;
    [SerializeField] private Canvas gameUICanvas;
    [SerializeField] private Canvas healthBarCanvas;

    [SerializeField] private GameObject mapManager;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject EnemyManagerPrefab;
    [SerializeField] private GameObject gameTimerPrefab;

    private GameObject player;
    private GameObject enemyManager;
    private GameObject gameTimer;

    public void ToTitleScreen()
    {
        
        if (player != null)
        {
            Destroy(player);
        }
        if (enemyManager != null)
        {
            Destroy(enemyManager);
        }
        gameUICanvas.gameObject.SetActive(false);
        menuUICanvas.gameObject.SetActive(true);

        Camera.main.GetComponent<CameraFollow>().Target = null;
        Camera.main.transform.position = new Vector3(0, 0, -1);
    }

    public void StartGame()
    {
        if (player != null)
        {
            Destroy(player);
        }
        player = Instantiate(playerPrefab);
        player.GetComponent<Health>().SetupHealthBar(healthBarCanvas);
        player.GetComponent<PlayerLevel>().SetupLevelHUD(gameUICanvas);

        if (enemyManager != null)
        {
            Destroy(enemyManager);
        }
        enemyManager = Instantiate(EnemyManagerPrefab);
        enemyManager.GetComponent<EnemyManager>().Target = player.transform;
        enemyManager.GetComponent<EnemyManager>().HealthBarCanvas = healthBarCanvas;

        gameUICanvas.gameObject.SetActive(true);
        menuUICanvas.gameObject.SetActive(false);

        gameTimer = Instantiate(gameTimerPrefab, gameUICanvas.transform);

        Camera.main.GetComponent<CameraFollow>().Target = player.transform;
    }

    private void Start()
    {
        ToTitleScreen();
    }
}
