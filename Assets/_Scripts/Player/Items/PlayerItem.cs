using UnityEngine;

public abstract class PlayerItem : MonoBehaviour
{
    public Sprite Sprite { get; }
    public int Level { get; private set; } = 1;
    public abstract int MaxLevel { get; }
    abstract protected void OnLevelUp();

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
