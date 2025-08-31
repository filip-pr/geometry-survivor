using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
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

    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private int baseLevelUpgradePoints = 50;
    [SerializeField] private int baseTimeSurvivedUpgradePoints = 50;

    private PlayerInput playerInput;
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
            Destroy(gameTimer);
        }
        foreach (Transform child in projectiles)
        {
            Destroy(child.gameObject);
        }
    }

    public void ReadyMainMenu()
    {
        gameUICanvas.gameObject.SetActive(false);
        menuUICanvas.gameObject.SetActive(true);
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
        player.GetComponent<PlayerInventory>().SetupItemSlots(gameUICanvas);
        player.GetComponent<PlayerController>().PlayerInput = playerInput;

        enemyManager = Instantiate(enemyManagerPrefab);
        enemyManager.GetComponent<EnemyManager>().Target = player.transform;
        enemyManager.GetComponent<EnemyManager>().HealthBarCanvas = healthBarCanvas;

        gameTimer = Instantiate(gameTimerPrefab, gameUICanvas.transform);

        ToPlayer();
    }

    private int CalculateUpgradePointsGain()
    {
        if (player == null) return 0;
        if (gameTimer == null) return 0;

        int levelsGained = player.GetComponent<PlayerLevel>().Level - 1;
        int minutesSurvived = Mathf.FloorToInt(gameTimer.GetComponent<Timer>().TimeElapsed / 60f);

        int levelGain = levelsGained * (baseLevelUpgradePoints * (levelsGained + 1)) / 2;
        int timeGain = minutesSurvived * (baseTimeSurvivedUpgradePoints * (minutesSurvived + 1)) / 2;

        return levelGain + timeGain;
    }

    public void EndGame()
    {
        int upgradePointsGained = CalculateUpgradePointsGain();
        upgradeManager.AddUpgradePoints(upgradePointsGained);
        ClearGame();
        ReadyMainMenu();
        ToTitleScreen();
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        ReadyMainMenu();
        ToTitleScreen();
    }
}
