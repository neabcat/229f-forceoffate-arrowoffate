using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    public void FadeToSceneByName(string sceneName)
    {
        fadeImage.gameObject.SetActive(true); // เปิดตอนกด
        StartCoroutine(FadeOut(sceneName));
    }

    void SetAlpha(float a)
    {
        Color c = fadeImage.color;
        c.a = a;
        fadeImage.color = c;
    }

    IEnumerator FadeOut(string sceneName)
    {
        float t = 0;

        SetAlpha(0f);

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            SetAlpha(t / fadeDuration);
            yield return null;
        }

        SetAlpha(1f);
        SceneManager.LoadScene(sceneName);
    }
}