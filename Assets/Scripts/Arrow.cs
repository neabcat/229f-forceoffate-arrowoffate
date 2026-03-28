using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasHit = false;
    [SerializeField] private Collider bowCollider;
    [SerializeField] private int damage = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (bowCollider != null)
            Physics.IgnoreCollision(GetComponent<Collider>(), bowCollider);

        Destroy(gameObject, 10f);
    }

    void Update()
    {
        if (rb == null || hasHit) return;
        if (rb.linearVelocity.magnitude > 0.1f)
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized, Vector3.up);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        SlimeController slime = collision.gameObject.GetComponent<SlimeController>();
        if (slime != null)
        {
            slime.TakeDamage(damage);
        }

        Ghost ghost = collision.gameObject.GetComponent<Ghost>();
        if (ghost != null)
        {
            ghost.ApplyArrowForce();
        }

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

    public void SetBowCollider(Collider bow)
    {
        bowCollider = bow;
    }
}