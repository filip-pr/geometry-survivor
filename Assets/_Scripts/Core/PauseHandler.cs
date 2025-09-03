using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script handling pausing and resuming of the game.
/// </summary>
public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private PlayerInput playerInput;

    private bool isPaused = false; 

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void PauseGame()
    {
        pauseMenu.transform.SetAsLastSibling();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    /// <summary>
    /// Resume the game.
    /// </summary>
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void OnDisable()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (playerInput.actions["Pause"].triggered)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
