using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [Header("HP")]
    public int maxHP = 3;
    private int currentHP;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float changeDirectionTime = 3f;

    [Header("Chase")]
    public float chaseSpeed = 5f;
    public float detectionRange = 20f;
    private Transform player;

    [Header("Bounce")]
    public Transform model;
    public float bounceHeight = 0.25f;
    public float bounceSpeed = 6f;

    [Header("Rotation")]
    public float rotateSpeed = 8f;
    public Vector3 rotationOffset = new Vector3(0, -90f, 0);

    private Rigidbody rb;
    private Vector3 moveDir;
    private Vector3 smoothDir;
    private float timer;

    private float bounceTimer;
    private bool isChasing = false;

    [Header("Hit Sounds")]
    public AudioClip[] hitClips;
    public UnityEngine.Audio.AudioMixerGroup sfxMixerGroup;
    private AudioSource audioSource;

    void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Start()
    {
        currentHP = maxHP;

        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        PickRandomDirection();
        smoothDir = moveDir;

        // ===== AUDIO SETUP =====
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;   // 3D sound
        audioSource.volume = 1f;
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 20f;
    }

    void FixedUpdate()
    {
        // detect player
        if (player != null)
        {
            Vector3 flatPlayer = player.position;
            flatPlayer.y = transform.position.y;

            float dist = Vector3.Distance(transform.position, flatPlayer);
            isChasing = dist <= detectionRange;
        }

        timer += Time.fixedDeltaTime;

        if (!isChasing && timer >= changeDirectionTime)
        {
            PickRandomDirection();
            timer = 0f;
        }

        Vector3 targetDir;
        float speed;

        if (isChasing && player != null)
        {
            targetDir = player.position - transform.position;
            targetDir.y = 0f;
            targetDir.Normalize();
            speed = chaseSpeed;
        }
        else
        {
            targetDir = moveDir;
            speed = moveSpeed;
        }

        // smooth direction
        smoothDir = Vector3.Lerp(smoothDir, targetDir, 6f * Time.fixedDeltaTime);

        // เดินตามพื้นจริง
        Vector3 velocity = smoothDir * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        // bounce เฉพาะ model
        if (model != null)
        {
            bounceTimer += Time.fixedDeltaTime * bounceSpeed;
            float bounceY = Mathf.Sin(bounceTimer) * bounceHeight;

            Vector3 local = model.localPosition;
            local.y = bounceY;
            model.localPosition = local;
        }

        // rotate smooth
        if (smoothDir != Vector3.zero)
        {
            Quaternion targetRot =
                Quaternion.LookRotation(-smoothDir) *
                Quaternion.Euler(rotationOffset);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotateSpeed * Time.fixedDeltaTime
            );
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        PlayHitSound();

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        PlayHitSound(); 

        GetComponent<Collider>().enabled = false;

        rb.linearVelocity = Vector3.zero;

        Destroy(gameObject, 0.5f);
    }

    void PickRandomDirection()
    {
        moveDir = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        ).normalized;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player p = col.gameObject.GetComponent<Player>();
            if (p != null)
                p.Die();
            return;
        }

        PickRandomDirection();
        timer = 0f;
    }
    void PlayHitSound()
    {
        if (hitClips == null || hitClips.Length == 0)
        {
            Debug.LogWarning("No hit sound on " + gameObject.name);
            return;
        }

        AudioClip clip = hitClips[Random.Range(0, hitClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}   