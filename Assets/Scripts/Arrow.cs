using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasHit = false;

    [SerializeField] private Collider bowCollider; // ลาก Bow มาใส่ใน Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (bowCollider != null)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), bowCollider);
        }

        Destroy(gameObject, 10f);
    }

    void Update()
    {
        if (rb == null || hasHit) return;

        if (rb.linearVelocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized, Vector3.up);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.SetParent(collision.transform);
        GetComponent<Collider>().enabled = false;
    }

    public void EnableColliderDelayed(float delay)
    {
        StartCoroutine(EnableColliderCoroutine(delay));
    }

    private IEnumerator EnableColliderCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = true;
    }
}