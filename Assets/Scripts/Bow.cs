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
        if (arrowPrefab == null || stringBone == null) return;

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
        if (arrowPrefab == null || stringBone == null) return;

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

            Vector3 dir = GetShootDir();
            rb.linearVelocity = dir * force;
            arrow.transform.rotation = cameraHolder.rotation * Quaternion.Euler(0f, 180f, -90f);

            // หน่วง enable Collider ให้ Arrow บินออกไปก่อน
            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if (arrowScript != null)
                arrowScript.EnableColliderDelayed(0.1f);
        }
    }

    void UpdateString()
    {
        if (stringBone == null || stringRestPoint == null || stringPullPoint == null) return;

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
            stringBone.position, targetPos, ref stringVelocity, 0.04f
        );
    }

    Vector3 GetTargetPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(
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