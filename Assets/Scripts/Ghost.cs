using UnityEngine;

public class Ghost : MonoBehaviour
{
    private ConstantForce cf;

    [Header("Hit Sounds")]
    public AudioClip[] hitClips;
    public UnityEngine.Audio.AudioMixerGroup sfxMixerGroup;
    private AudioSource audioSource;

    void Start()
    {
        cf = GetComponent<ConstantForce>();
        if (cf == null)
            cf = gameObject.AddComponent<ConstantForce>();
        cf.force = Vector3.zero;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
        
    }

    public void ApplyArrowForce()
    {
        cf.force = new Vector3(0f, 0f, -9f);

        if (hitClips != null && hitClips.Length > 0)
        {
            AudioClip clip = hitClips[Random.Range(0, hitClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}