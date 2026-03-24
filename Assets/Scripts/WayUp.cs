using UnityEngine;
using System.Collections;

public class WayUp : MonoBehaviour
{
    public GameObject targetObject;
    public float targetY = 4.5f;
    public float speed = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ghost>() == null) return;
        if (targetObject != null)
            StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
        Vector3 startPos = targetObject.transform.position;
        Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            targetObject.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        targetObject.transform.position = endPos;
    }
}