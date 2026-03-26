using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuUI : MonoBehaviour
{
    public GameObject deathMenu;

    void Start()
    {
        deathMenu.SetActive(false);
    }

    public void Show()
    {
        deathMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f; // pause à¡Á
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}