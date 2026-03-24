using UnityEngine;
public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public bool smoothFollow = false;
    public float smoothSpeed = 20f;

    // CameraFollower.cs
    void LateUpdate()
    {
        if (target == null) return;
        if (smoothFollow)
            transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);
        else
            transform.position = target.position;
        transform.rotation = target.rotation;
    }
}