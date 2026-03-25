using UnityEngine;

public class BowAim : MonoBehaviour
{
    public Transform cameraHolder;
    public Vector3 positionOffset = new Vector3(0.3f, -0.3f, 0.6f);
    public Vector3 rotationOffset = Vector3.zero;

    public float followSpeed = 0.01f; // ค่ายิ่งต่ำยิ่งตามเร็ว

    private Vector3 velocity;

    void LateUpdate()
    {
        if (cameraHolder == null) return;

        Vector3 targetPos = cameraHolder.position +
                            cameraHolder.TransformDirection(positionOffset);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            followSpeed
        );

        transform.rotation =
            cameraHolder.rotation *
            Quaternion.Euler(rotationOffset.x, 180f + rotationOffset.y, -90f + rotationOffset.z);
    }
}