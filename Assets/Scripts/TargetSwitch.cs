using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetSwitch : MonoBehaviour
{
    [Header("Floor & Physics")]
    public Collider floorCollider;
    public Renderer floorRenderer;
    public Material newVisualMaterial;
    public PhysicsMaterial newPhysicMaterial;
    public GameObject cube;
    public Door door;

    [Header("Hit Sounds")]
    public AudioClip[] hitClips;
    public UnityEngine.Audio.AudioMixerGroup sfxMixerGroup;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hitClips != null && hitClips.Length > 0)
        {
            AudioClip clip = hitClips[UnityEngine.Random.Range(0, hitClips.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (collision.gameObject)
        {
            floorCollider.sharedMaterial = newPhysicMaterial;
            floorRenderer.material = newVisualMaterial;
            Destroy(cube.gameObject);
        }

        if (collision.gameObject.CompareTag("Taget1"))
        {
            floorCollider.sharedMaterial = newPhysicMaterial;
            floorRenderer.material = newVisualMaterial;
        }

        if ((collision.gameObject.CompareTag("Target2")))
        {
            floorCollider.sharedMaterial = newPhysicMaterial;
            floorRenderer.material = newVisualMaterial;
        }

    }

    
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ResetScene();
        }

    }

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
