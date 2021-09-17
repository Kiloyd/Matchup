using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void gameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void gameReload()
    {
        SceneManager.LoadScene(0);
    }

    public void finishCancel()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
