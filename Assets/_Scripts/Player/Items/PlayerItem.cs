using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerItem : MonoBehaviour
{
    public Sprite Sprite { get; }
    public int Level { get; private set; } = 0;
    public abstract int MaxLevel { get; }

    abstract protected void OnLevelUp();

    private void Start()
    {
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
