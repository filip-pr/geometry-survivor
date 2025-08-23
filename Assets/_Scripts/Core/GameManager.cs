using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas menuUICanvas;
    [SerializeField] private Canvas gameUICanvas;
    [SerializeField] private Canvas healthBarCanvas;

    [SerializeField] private Transform titleScreen;
    [SerializeField] private Transform shopScreen;
    [SerializeField] private Transform projectiles;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyManagerPrefab;
    [SerializeField] private GameObject gameTimerPrefab;

    private GameObject player;
    private GameObject enemyManager;
    private GameObject gameTimer;

    public void ToTitleScreen()
    {
        Camera.main.GetComponent<CameraFollow>().Target = titleScreen;
    }

    public void ToShop()
    {
        Camera.main.GetComponent<CameraFollow>().Target = shopScreen;
    }

    public void ToPlayer()
    {
        if (player != null)
        {
            Camera.main.GetComponent<CameraFollow>().Target = player.transform;
        }
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    private void ClearGame()
    {
        if (player != null)
        {
            Destroy(player);
        }
        if (enemyManager != null)
        {
            Destroy(enemyManager);
        }
        if (gameTimer != null)
        {
            Destroy(enemyManager);
        }
    }

    public void StartGame()
    {
        ClearGame();

        gameUICanvas.gameObject.SetActive(true);
        menuUICanvas.gameObject.SetActive(false);

        player = Instantiate(playerPrefab, titleScreen.position, Quaternion.identity);
        player.GetComponent<Health>().SetupHealthBar(healthBarCanvas);
        player.GetComponent<PlayerLevel>().SetupLevelHUD(gameUICanvas);
        player.GetComponent<PlayerInventory>().ProjectileParent = projectiles;

        enemyManager = Instantiate(enemyManagerPrefab);
        enemyManager.GetComponent<EnemyManager>().Target = player.transform;
        enemyManager.GetComponent<EnemyManager>().HealthBarCanvas = healthBarCanvas;

        gameTimer = Instantiate(gameTimerPrefab, gameUICanvas.transform);

        ToPlayer();
    }

    public void EndGame()
    {
        ClearGame();

        gameUICanvas.gameObject.SetActive(false);
        menuUICanvas.gameObject.SetActive(true);

        ToTitleScreen();
    }

    private void Start()
    {
        ToTitleScreen();
    }
}
