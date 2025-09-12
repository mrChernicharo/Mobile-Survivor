using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] Button playButton;
    void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(GameStart);
        }
    }


    void GameStart()
    {
        PauseManager.isPaused = false;
        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        yield return SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(1);
    }

}
