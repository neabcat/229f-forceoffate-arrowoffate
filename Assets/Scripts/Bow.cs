using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [Header("--- Arrow ---")]
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;

    [Header("--- StringBone ---")]
    public Transform stringBone;
    public Transform stringRestPoint;
    public Transform stringPullPoint;

    [Header("--- Fix Direction ---")]
    public bool invertShoot = false;

    [Header("--- Charge ---")]
    public float maxChargeTime = 2f;
    public float minForce = 10f;
    public float maxForce = 50f;

    [Header("--- String Return ---")]
    public float stringReturnSpeed = 10f;

    private float chargeTime = 0f;
    private bool isCharging = false;
    private bool hasFired = false;
    private Vector3 stringVelocity = Vector3.zero;
    private GameObject nockArrow;

    void Update()
    {
        HandleInput();
        UpdateString();
        UpdateNockArrow();
    }

    void HandleInput()
    {
        bool pressing = Mouse.current.leftButton.isPressed;

        if (pressing && !hasFired)
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
        if (arrowPrefab == null || arrowSpawnPoint == null) return;

        nockArrow = Instantiate(arrowPrefab, stringBone.position, GetArrowRotation());

        Rigidbody rb = nockArrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

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
        if (arrowPrefab == null || arrowSpawnPoint == null) return;

        float percent = Mathf.Clamp01(chargeTime / maxChargeTime);
        float force = Mathf.Lerp(minForce, maxForce, percent);

        GameObject arrow = nockArrow != null
            ? nockArrow
            : Instantiate(arrowPrefab, stringBone.position, GetArrowRotation());
        nockArrow = null;

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            Collider col = arrow.GetComponent<Collider>();
            if (col != null) col.enabled = true;

            Vector3 dir = invertShoot ? arrowSpawnPoint.forward : -arrowSpawnPoint.forward;
            rb.linearVelocity = dir * force;

            arrow.transform.rotation = GetArrowRotation();
        }
        else
        {
            Debug.LogWarning("❌ Arrow prefab no Rigidbody");
        }
    }

    void UpdateString()
    {
        if (stringBone == null || stringRestPoint == null || stringPullPoint == null)
            return;

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

        stringBone.position = Vector3.SmoothDamp(
            stringBone.position,
            targetPos,
            ref stringVelocity,
            0.04f
        );
    }

    Quaternion GetArrowRotation()
    {
        Vector3 dir = invertShoot ? arrowSpawnPoint.forward : -arrowSpawnPoint.forward;
        return Quaternion.LookRotation(dir) * Quaternion.Euler(-180f, -180, 90);
    }
}