using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject inGameUI;
    public GameObject gameOverMenu;


    private void Awake()
    {
        mainMenu.SetActive(true);
        inGameUI.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartButton()
    {
        mainMenu.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void QuitButton()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
