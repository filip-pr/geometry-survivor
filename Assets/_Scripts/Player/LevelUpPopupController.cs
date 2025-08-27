using UnityEngine;

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
