using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 3f;

    [Header("Bounce")]
    public float bounceHeight = 1.7f;
    public float bounceSpeed = 6f;

    [Header("Rotation Offset")]
    public Vector3 rotationOffset = new Vector3(0, -90f, 0);

    private Rigidbody rb;
    private Vector3 moveDir;
    private float timer;

    private float baseY;
    private float bounceTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.useGravity = false; // 🔥 สำคัญ
        }

        baseY = transform.position.y;

        PickRandomDirection();
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= changeDirectionTime)
        {
            PickRandomDirection();
            timer = 0f;
        }

        // 🔥 เด้งแกน Y
        bounceTimer += Time.fixedDeltaTime * bounceSpeed;
        float bounceY = Mathf.Sin(bounceTimer) * bounceHeight;

        // 🔥 เดิน
        Vector3 move = moveDir * moveSpeed * Time.fixedDeltaTime;
        Vector3 nextPos = rb.position + move;

        // ใส่ Y เด้งเข้าไป
        nextPos.y = baseY + bounceY;

        rb.MovePosition(nextPos);

        // 🔥 หมุน (หน้า -X)
        if (moveDir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(-moveDir);
            transform.rotation = rot * Quaternion.Euler(rotationOffset);
        }
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
        if (!col.gameObject.CompareTag("Player"))
        {
            // 🔥 ชน → เปลี่ยนทิศทันที
            PickRandomDirection();
            timer = 0f;
        }
    }
}