using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float detectionRadius = 10f;
    private ConstantForce cf;

    void Start()
    {
        cf = GetComponent<ConstantForce>();
        if (cf == null)
            cf = gameObject.AddComponent<ConstantForce>();
    }

    void Update()
    {
        Arrow[] arrows = FindObjectsByType<Arrow>(FindObjectsSortMode.None);
        bool foundArrow = false;

        foreach (Arrow arrow in arrows)
        {
            float dist = Vector3.Distance(transform.position, arrow.transform.position);
            if (dist <= detectionRadius)
            {
                foundArrow = true;
                break;
            }
        }

        cf.force = foundArrow ? new Vector3(0f, 0f, -9f) : Vector3.zero;
        Debug.Log("foundArrow: " + foundArrow + " | cf.force: " + cf.force);
    }
}