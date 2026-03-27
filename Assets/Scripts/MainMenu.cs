using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Game";
    public SceneFader fader; // ลาก SceneFader มาใส่

    public GameObject uiOption;

    public Slider Music;
    public Slider SFX;
    public AudioMixer mainAuio;

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

    public void Back()
    {
        uiOption.SetActive(false);
    }

    public void Options(float valum)
    {
        Debug.Log("Open Options Menu");
        uiOption.gameObject.SetActive(true);
    }

    public void ChangeMusicVolume()
    {
        mainAuio.SetFloat("Music", Music.value);
    }

    public void ChangeSFXVolume()
    {
        mainAuio.SetFloat("SFX", SFX.value);
    }
}