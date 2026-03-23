using UnityEngine;
public class BowAim : MonoBehaviour
{
    public Transform cameraHolder;
    public Vector3 positionOffset = new Vector3(0.3f, -0.3f, 0.6f);
    public Vector3 rotationOffset = Vector3.zero;

    void LateUpdate()
    {
        if (cameraHolder == null) return;

        Vector3 targetPos = cameraHolder.position + cameraHolder.TransformDirection(positionOffset);
        Quaternion targetRot = cameraHolder.rotation
                             * Quaternion.Euler(rotationOffset.x, 180f + rotationOffset.y, -90f + rotationOffset.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 30f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 30f);
    }
}