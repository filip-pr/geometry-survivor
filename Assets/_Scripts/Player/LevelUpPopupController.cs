using UnityEngine;

/// <summary>
/// Script to handle level up popup behavior, pausing the game when active.
/// </summary>
public class LevelUpPopupController : MonoBehaviour
{
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
}
