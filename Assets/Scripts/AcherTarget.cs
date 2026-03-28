using UnityEngine;

public class AcherTarget : MonoBehaviour
{
    float force;
    float acceleration;
    [SerializeField] float forceSpin;

    public GameObject weight;
    public GameObject Wall;
    public GameObject rock;
    private bool isMoving = false;
    private float moveTimer = 0f;
    private float moveDuration = 15f;

    [Header("Collision Sounds")]
    public AudioClip[] collisionClips;
    public UnityEngine.Audio.AudioMixerGroup sfxMixerGroup;
    private AudioSource audioSource;

    private void Start()
    {
        forceSpin = 0f;
        force = 0f;
        acceleration = 0f;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
    }

    private void Update()
    {
        if (isMoving)
        {
            moveTimer += Time.deltaTime;
            rock.transform.Translate(Vector3.down * 2.5f * Time.deltaTime);

            if (moveTimer >= moveDuration)
            {
                isMoving = false;
            }
        }
    }

    void CalculateForce()
    {
        Rigidbody rb = weight.GetComponent<Rigidbody>();
        float mass = rb.mass;
        force = mass * acceleration;
        rb.AddForce(new Vector3(force, force, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test");

        if (collisionClips != null && collisionClips.Length > 0)
        {
            AudioClip clip = collisionClips[UnityEngine.Random.Range(0, collisionClips.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (gameObject.CompareTag("Taget1"))
        {
            weight.GetComponent<Renderer>().material.color = Color.blue;
            Debug.Log("1");
        }
        else if (gameObject.CompareTag("Target2"))
        {
            forceSpin = 6000;
            Rigidbody rb = weight.GetComponent<Rigidbody>();
            rb.AddTorque(Vector3.up * forceSpin);

            Debug.Log("2");
        }
        else if (gameObject.CompareTag("Target3"))
        {
            acceleration = 600f;
            CalculateForce();
            Debug.Log("3");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Rock"))
        {
            Debug.Log("Can");
            isMoving = true;
            moveTimer = 0f;
        }
    }

    private void StopRock()
    {
        Rigidbody rb = rock.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

}
