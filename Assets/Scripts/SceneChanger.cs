using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    public SceneFader fader;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fader.FadeToSceneByName(sceneName);
        }
    }
}
