using UnityEngine;

public class SlimeWaveGate : MonoBehaviour
{
    [Header("Wall")]
    public Transform wall;
    public float moveDownDistance = 5f;
    public float moveSpeed = 2f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool open = false;

    void Start()
    {
        startPos = wall.position;
        targetPos = startPos - new Vector3(0, moveDownDistance, 0);
    }

    void Update()
    {
        // เช็คว่ายังมี slime อยู่ไหม
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");

        if (!open && slimes.Length == 0)
        {
            open = true;
        }

        // เลื่อนกำแพง
        if (open)
        {
            wall.position = Vector3.MoveTowards(
                wall.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
        }
    }
}