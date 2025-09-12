using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseManager : MonoBehaviour
{

    public static bool isPaused = false;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private GameObject mainMenuButtonContainer;
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject pauseMenuUI;

    void Start()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
    }

    void TogglePause()
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



    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        mainMenuButtonContainer.SetActive(true);

        mainMenuButton.enabled = true;
        joystick.enabled = false;

        Time.timeScale = 0f;
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        mainMenuButtonContainer.SetActive(false);

        mainMenuButton.enabled = false;
        joystick.enabled = true;

        Time.timeScale = 1f;
        isPaused = false;


    }

    void GoToMainMenu()
    {
        Debug.Log("Go to Main Menu!");
        StartCoroutine(GoToMainMenuCoroutine());
    }

    private IEnumerator GoToMainMenuCoroutine()
    {
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(1);
        yield return unloadOp;
        SceneManager.LoadScene(0);
    }
}
