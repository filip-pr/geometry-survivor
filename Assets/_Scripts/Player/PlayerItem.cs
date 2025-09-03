using UnityEngine;

/// <summary>
/// Abstract base class for player items that can be leveled up and modify player stats.
/// </summary>
public abstract class PlayerItem : MonoBehaviour
{
    abstract public string ItemName { get; }
    public int Level { get; private set; } = 1;
    public abstract int MaxLevel { get; }
    public PlayerStats PlayerStats { get; set; }
    [field: SerializeField] public Transform ProjectileParent { get; set; }
    abstract protected void OnLevelUp();

    private void Awake()
    {
        PlayerStats playerStats = GetComponentInParent<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerItem could not find PlayerStats component in parent.");
        }
        else
        {
            PlayerStats = playerStats;
        }
    }

    public abstract void SetupModifiers(PlayerStats playerStats);

    private void Start()
    {
        OnLevelUp();
    }

    public void LevelUp()
    {
        if (Level >= MaxLevel)
        {
            return;
        }
        Level++;
        OnLevelUp();
    }
}
