using UnityEngine;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private PlayerInput playerInput;

    private bool isPaused = false; 


    public void PauseGame()
    {
        pauseMenu.transform.SetAsLastSibling();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

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
