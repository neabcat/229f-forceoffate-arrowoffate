using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 9f;

    [Header("Jump")]
    public float jumpForce = 5f;
    public float gravityScale = 2.5f;

    [Header("Mouse Look")]
    public Transform cameraHolder;
    public float mouseSensitivity = 0.15f;
    public float maxLookAngle = 80f;

    [Header("Footstep Sounds")]
    public AudioClip[] footstepClips;
    public float footstepInterval = 0.45f;
    public float sprintStepInterval = 0.3f;
    public UnityEngine.Audio.AudioMixerGroup sfxMixerGroup;

    private AudioSource audioSource;
    public AudioClip[] deathClips;
    private float footstepTimer = 0f;

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool isGrounded = false;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    public DeathMenuUI deathMenu;
    public GameObject uiESC;
    private bool isDead = false;
    private Vector2 smoothedDelta;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
    }

    void Update()
    {
        if (isDead) return;
        HandleMouseLook();
        HandleJump();

        if (Input.GetKey(KeyCode.Escape))
        {
            uiESC.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;
        HandleMovement();
        ApplyGravity();
        HandleFootsteps();
    }

    void HandleMouseLook()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        mouseDelta.x = Mathf.Clamp(mouseDelta.x, -50f, 50f);
        mouseDelta.y = Mathf.Clamp(mouseDelta.y, -50f, 50f);

        smoothedDelta = Vector2.Lerp(smoothedDelta, mouseDelta, 0.5f);

        transform.Rotate(Vector3.up * smoothedDelta.x * mouseSensitivity);
        xRotation -= smoothedDelta.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleFootsteps()
    {
        if (footstepClips == null || footstepClips.Length == 0) return;

        Vector2 input = moveAction.ReadValue<Vector2>();
        bool isMoving = input.magnitude > 0.1f && isGrounded;

        if (isMoving)
        {
            bool sprinting = sprintAction.IsPressed();
            float interval = sprinting ? sprintStepInterval : footstepInterval;

            footstepTimer -= Time.fixedDeltaTime;
            if (footstepTimer <= 0f)
            {
                AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
                audioSource.PlayOneShot(clip);
                footstepTimer = interval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

    void HandleJump()
    {
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleMovement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        bool sprinting = sprintAction.IsPressed();
        float speed = sprinting ? sprintSpeed : moveSpeed;
        Vector3 moveDir = (transform.right * input.x + transform.forward * input.y).normalized;
        Vector3 targetVelocity = moveDir * speed;
        targetVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = targetVelocity;
    }

    void ApplyGravity()
    {
        rb.linearVelocity += Physics.gravity * (gravityScale - 1f) * Time.fixedDeltaTime;
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        Debug.Log("Player Died!");
        rb.linearVelocity = Vector3.zero;
        if (deathClips != null && deathClips.Length > 0)
        {
            AudioClip clip = deathClips[Random.Range(0, deathClips.Length)];
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
        if (deathMenu != null)
            deathMenu.Show();
        gameObject.SetActive(false);
    }
}