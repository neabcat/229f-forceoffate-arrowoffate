using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuUI : MonoBehaviour
{
    public GameObject deathMenu;
    public SceneFader fader;
    public string mainMenuScene = "MainMenu";

    void Start()
    {
        deathMenu.SetActive(false);
    }

    public void Show()
    {
        deathMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        fader.FadeToSceneByName(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        fader.FadeToSceneByName(mainMenuScene);
    }
}