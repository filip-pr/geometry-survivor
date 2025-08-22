using UnityEngine;

public abstract class PlayerItem : MonoBehaviour
{
    public Sprite Sprite { get; }
    public int Level { get; private set; } = 0;
    public abstract int MaxLevel { get; }
    public PlayerStats PlayerStats { get; set; }
    abstract protected void OnLevelUp();

    private void Start()
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
        LevelUp();
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
