using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script handling the overall game state and flow.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas menuUICanvas;
    [SerializeField] private Canvas gameUICanvas;
    [SerializeField] private Canvas healthBarCanvas;

    [SerializeField] private Transform titleScreen;
    [SerializeField] private Transform shopScreen;
    [SerializeField] private Transform upgradePointsEndScreen;
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

    /// <summary>
    /// Set the camera position to the title screen.
    /// </summary>
    public void ToTitleScreen()
    {
        Camera.main.GetComponent<CameraFollow>().Target = titleScreen;
    }

    /// <summary>
    /// Set the camera position to the upgrade shop screen.
    /// </summary>
    public void ToShopScreen()
    {
        Camera.main.GetComponent<CameraFollow>().Target = shopScreen;
    }

    /// <summary>
    /// Set the camera position to the upgrade points end screen.
    /// </summary>
    public void ToUpgradePointsEndScreen()
    {
        Camera.main.GetComponent<CameraFollow>().Target = upgradePointsEndScreen;
    }

    /// <summary>
    /// Set the camera position to the player.
    /// </summary>
    public void ToPlayer()
    {
        if (player != null)
        {
            Camera.main.GetComponent<CameraFollow>().Target = player.transform;
        }
    }

    /// <summary>
    /// Exit the application.
    /// </summary>
    public void ExitApp()
    {
        Application.Quit();
    }

    /// <summary>
    /// Clear all game instance objects.
    /// </summary>
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

    /// <summary>
    /// Prepare the UI for the main menu.
    /// </summary>
    public void ReadyMainMenu()
    {
        gameUICanvas.gameObject.SetActive(false);
        menuUICanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// Start a new game instance.
    /// </summary>
    public void StartGame()
    {
        ClearGame();

        gameUICanvas.gameObject.SetActive(true);
        menuUICanvas.gameObject.SetActive(false);

        player = Instantiate(playerPrefab, titleScreen.position, Quaternion.identity);
        player.GetComponent<PlayerStats>().SetUpgradeModifiers(upgradeManager);
        player.GetComponent<PlayerHealth>().SetupHealthBar(healthBarCanvas);
        player.GetComponent<PlayerHealth>().SetupDeath(this);
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

    /// <summary>
    /// Calculate and update upgrade points based on the performance in the last game instance.
    /// </summary>
    private void UpdateUpgradePoints()
    {
        int levelsGained = player.GetComponent<PlayerLevel>().Level - 1;
        int minutesSurvived = Mathf.FloorToInt(gameTimer.GetComponent<Timer>().TimeElapsed / 60f);

        int levelGain = levelsGained * (baseLevelUpgradePoints * (levelsGained + 1)) / 2;
        int timeGain = minutesSurvived * (baseTimeSurvivedUpgradePoints * (minutesSurvived + 1)) / 2;
        int total = levelGain + timeGain;

        StatModifier upgradePointsModifier = new StatModifier();
        upgradePointsModifier.IncreaseMultiplier(upgradeManager.GetUpgradeHandler("UpgradePointsGainUpgrade").UpgradeAmount / 100f);
        
        int totalWithBonus = Mathf.CeilToInt(upgradePointsModifier.Modify(total));
        
        upgradePointsEndScreen.Find("UpgradePointsGainText").GetComponent<TextMeshProUGUI>().text = $"{timeGain}\n{levelGain}\n{totalWithBonus-total}\n{totalWithBonus}";
        upgradeManager.AddUpgradePoints(totalWithBonus);
    }

    /// <summary>
    /// End the game instance, update upgrade points, and return to the main menu.
    /// </summary>
    public void EndGame()
    {
        UpdateUpgradePoints();
        ClearGame();
        ReadyMainMenu();
        ToUpgradePointsEndScreen();
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        ReadyMainMenu();
        ToTitleScreen();
    }
}
