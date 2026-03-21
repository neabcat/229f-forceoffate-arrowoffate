using UnityEngine;

public class BowAim : MonoBehaviour
{
    public Transform cameraHolder;

    public Vector3 rotationOffset = Vector3.zero;

    void LateUpdate()
    {
        if (cameraHolder == null) return;

        transform.rotation = Quaternion.Euler(
            -cameraHolder.eulerAngles.x,
            cameraHolder.eulerAngles.y + 180f,
            90f
        );
    }
}