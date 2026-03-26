using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public enum SlimeState { Patrol, Chase }

    [Header("Patrol")]
    public BoxCollider patrolArea;
    public float patrolSpeed = 3f;
    public float patrolWaitTime = 1f;

    [Header("Chase")]
    public float chaseSpeed = 5f;
    public float detectionRange = 8f;

    [Header("Bounce")]
    public float bounceHeight = 0.8f;   // ความสูงเด้ง
    public float bounceSpeed = 6f;      // ความเร็วเด้ง

    [Header("References")]
    public Transform player;

    private SlimeState currentState = SlimeState.Patrol;
    private Vector3 targetPoint;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    private Rigidbody rb;
    private Animator anim;

    private float baseY;
    private float bounceTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        // ล็อคแค่การหมุน
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        // เก็บตำแหน่ง Y เริ่มต้น
        baseY = transform.position.y;

        // หา player อัตโนมัติ
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        SetNewRandomTarget();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case SlimeState.Patrol:
                HandlePatrol();

                if (distanceToPlayer <= detectionRange)
                {
                    currentState = SlimeState.Chase;
                    isWaiting = false;
                }
                break;

            case SlimeState.Chase:
                HandleChase();

                if (distanceToPlayer > detectionRange * 1.5f)
                {
                    currentState = SlimeState.Patrol;
                    SetNewRandomTarget();
                }
                break;
        }
    }

    void HandlePatrol()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f)
            {
                isWaiting = false;
                SetNewRandomTarget();
            }

            SetAnimation(false);
            return;
        }

        MoveTowards(targetPoint, patrolSpeed);

        if (Vector3.Distance(transform.position, targetPoint) < 0.5f)
        {
            isWaiting = true;
            waitTimer = patrolWaitTime;
        }
    }

    void HandleChase()
    {
        MoveTowards(player.position, chaseSpeed);
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position);
        dir.y = 0f;

        if (dir.magnitude < 0.1f)
        {
            SetAnimation(false);
            return;
        }

        dir.Normalize();

        // 🔥 หมุนก่อน
        Quaternion targetRot = Quaternion.LookRotation(dir);
        targetRot *= Quaternion.Euler(0, -90f, 0); // แปลง Z → -X

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            8f * Time.deltaTime
        );

        // 🔥 ใช้ "forward ของโมเดลจริง"
        Vector3 move = -transform.right * speed * Time.deltaTime;

        // เด้ง
        bounceTimer += Time.deltaTime * bounceSpeed;
        float bounceY = Mathf.Abs(Mathf.Sin(bounceTimer)) * bounceHeight;

        Vector3 newPos = transform.position + move;
        newPos.y = baseY + bounceY;

        if (rb != null)
            rb.MovePosition(newPos);
        else
            transform.position = newPos;

        SetAnimation(true);
    }

    void SetAnimation(bool isMoving)
    {
        if (anim != null)
            anim.SetBool("isMoving", isMoving);
    }

    void SetNewRandomTarget()
    {
        if (patrolArea == null) return;

        Bounds bounds = patrolArea.bounds;

        targetPoint = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            baseY,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
                playerScript.Die();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange * 1.5f);
    }
}