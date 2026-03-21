using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("=== Target ===")]
    public Transform target; // ลาก CameraHolder ใส่

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}