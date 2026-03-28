using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using Unity.VectorGraphics;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Game";
    public SceneFader fader;

    public GameObject uiOption;

    public Slider Music;
    public Slider SFX;
    public AudioMixer mainAuio;
    public AudioSource uiAudio;

    public AudioClip start;
    public AudioClip option;
    public AudioClip back;
    public AudioClip quit;

    void Awake()
    {
        // กันลืม assign
        if (uiAudio == null)
            uiAudio = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        uiAudio.PlayOneShot(start);
        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(0.3f); // รอเสียงก่อนเปลี่ยนฉาก
        fader.FadeToSceneByName(gameSceneName);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitRoutine());
    }

    IEnumerator QuitRoutine()
    {
        uiAudio.PlayOneShot(quit);
        yield return new WaitForSecondsRealtime(0.3f);
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Back()
    {

        uiAudio.PlayOneShot(back);
        uiOption.SetActive(false);
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Options(float valum)
    {
        uiAudio.PlayOneShot(option);
        uiOption.SetActive(true);
    }

    public void ChangeMusicVolume() 
    { mainAuio.SetFloat("Music", Music.value); 
    }
    public void ChangeSFXVolume() 
    { mainAuio.SetFloat("SFX", SFX.value); 
    }
}