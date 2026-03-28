using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [Header("--- Arrow ---")]
    public GameObject arrowPrefab;

    [Header("--- Camera ---")]
    public Transform cameraHolder;

    [Header("--- StringBone ---")]
    public Transform stringBone;
    public Transform stringRestPoint;
    public Transform stringPullPoint;

    [Header("--- Charge ---")]
    public float maxChargeTime = 2f;
    public float minForce = 10f;
    public float maxForce = 50f;

    [Header("--- String Return ---")]
    public float stringReturnSpeed = 10f;

    [Header("--- Bow Sounds ---")]
    public AudioClip[] shootClips;
    public UnityEngine.Audio.AudioMixerGroup sfxMixerGroup;
    private AudioSource audioSource;

    private float chargeTime = 0f;
    private bool isCharging = false;
    private bool hasFired = false;
    private GameObject nockArrow;
    private float fireCooldown = 0f;

    private Camera cam;

    void Awake()
    {
        cam = Camera.main;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
        
    }

    void Update()
    {
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    void LateUpdate()
    {
        HandleInput();
        UpdateString();
        UpdateNockArrow();
    }

    void HandleInput()
    {

        if (Cursor.lockState != CursorLockMode.Locked)
            return;
        bool pressing = Mouse.current.leftButton.isPressed;

        if (pressing && !hasFired && fireCooldown <= 0f)
        {
            if (!isCharging && nockArrow == null)
                SpawnNockArrow();

            isCharging = true;
            chargeTime = Mathf.Min(chargeTime + Time.deltaTime, maxChargeTime);
        }

        if (!pressing && isCharging)
        {
            isCharging = false;
            hasFired = true;
            Shoot();
        }
    }

    void SpawnNockArrow()
    {
        if (arrowPrefab == null || stringBone == null) return;

        nockArrow = Instantiate(arrowPrefab, stringBone.position, GetArrowRotation());

        Rigidbody rb = nockArrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Collider bowCol = GetComponent<Collider>();
        Arrow arrowScript = nockArrow.GetComponent<Arrow>();
        if (bowCol != null && arrowScript != null)
            arrowScript.SetBowCollider(bowCol);

        Collider col = nockArrow.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    void UpdateNockArrow()
    {
        if (nockArrow == null || !isCharging) return;

        nockArrow.transform.position = stringBone.position;
        nockArrow.transform.rotation = GetArrowRotation();
    }

    void Shoot()
    {
        fireCooldown = 0.4f;
        float percent = Mathf.Clamp01(chargeTime / maxChargeTime);
        float force = Mathf.Lerp(minForce, maxForce, percent);

        GameObject arrow = nockArrow;
        nockArrow = null;

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            Vector3 dir = GetShootDir();
            rb.AddForce(dir * force, ForceMode.Impulse);
        }

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
            arrowScript.EnableColliderDelayed(0.1f);

        // ── เพิ่ม ────────────────────────────────────────────────
        if (shootClips != null && shootClips.Length > 0)
        {
            AudioClip clip = shootClips[Random.Range(0, shootClips.Length)];
            audioSource.PlayOneShot(clip);
        }
        // ─────────────────────────────────────────────────────────
    }

    void UpdateString()
    {
        if (stringBone == null) return;

        if (!isCharging && chargeTime > 0f)
        {
            chargeTime -= Time.deltaTime * stringReturnSpeed;
            if (chargeTime <= 0f)
            {
                chargeTime = 0f;
                hasFired = false;
            }
        }

        float t = Mathf.Clamp01(chargeTime / maxChargeTime);
        Vector3 targetPos = Vector3.Lerp(stringRestPoint.position, stringPullPoint.position, t);

        stringBone.position = Vector3.Lerp(
            stringBone.position,
            targetPos,
            15f * Time.deltaTime
        );
    }

    Vector3 GetTargetPoint()
    {
        Ray ray = cam.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
            return hit.point;

        return ray.origin + ray.direction * 500f;
    }

    Vector3 GetShootDir()
    {
        return (GetTargetPoint() - stringBone.position).normalized;
    }

    Quaternion GetArrowRotation()
    {
        return cameraHolder.rotation * Quaternion.Euler(180f, 180f, -90f);
    }
}