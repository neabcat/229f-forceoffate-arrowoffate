using System.Collections;
using UnityEngine;

public class Minus : MonoBehaviour
{
    public float force;
    public float spin;
    public float magnus;
    public bool isTake = false;
    public CapsuleCollider ghostBox;
    Rigidbody rb;
    public GameObject lift;

    public AudioClip triggerClips;
    public UnityEngine.Audio.AudioMixerGroup sfxMixerGroup;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ghostBox.isTrigger = true;

        audioSource = gameObject.AddComponent<AudioSource>(); // 👈 เพิ่ม
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTake)
        {
            Magnus();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Magnus");
            lift.SetActive(true);
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Target2"))
        {
            
            Debug.Log("-*-");
            isTake = true;

            StartCoroutine(GhostTrigger());
        }
    }

    IEnumerator GhostTrigger()
    {
        
        yield return new WaitForSeconds(0.2f);

        ghostBox.isTrigger = false;

        audioSource.PlayOneShot(triggerClips);
        
    }

    void Magnus()
    {
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        rb.AddTorque(Vector3.up * spin);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        Vector3 spin = rb.angularVelocity;

        Vector3 magnusForce = magnus * Vector3.Cross(velocity, spin);

        rb.AddForce(magnusForce);
    }
}
