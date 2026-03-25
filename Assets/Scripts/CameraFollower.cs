using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public bool smoothFollow = true;
    public float smoothSpeed = 20f;

    void LateUpdate()
    {
        if (target == null) return;

        if (smoothFollow)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                target.position,
                smoothSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                target.rotation,
                smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}