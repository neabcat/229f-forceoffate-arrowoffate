using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public bool smoothFollow = false;
    public float smoothSpeed = 20f;

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}