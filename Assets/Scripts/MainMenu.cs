using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Game";
    public SceneFader fader; // ลาก SceneFader มาใส่

    public void StartGame()
    {
        Time.timeScale = 1f;
        fader.FadeToSceneByName(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Options()
    {
        Debug.Log("Open Options Menu");
    }
}