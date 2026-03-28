using UnityEngine;

public class Credit : MonoBehaviour
{
    public RollingText UIcredit;
    public GameObject UI;

    public AudioClip creditSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        UI.SetActive(true);
        UIcredit.Rolling();

        audioSource.PlayOneShot(creditSound); 
    }
}