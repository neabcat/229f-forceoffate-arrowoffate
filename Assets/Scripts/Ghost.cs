using UnityEngine;

public class Ghost : MonoBehaviour
{
    private ConstantForce cf;

    void Start()
    {
        cf = GetComponent<ConstantForce>();
        if (cf == null)
            cf = gameObject.AddComponent<ConstantForce>();

        cf.force = Vector3.zero;
    }

    public void ApplyArrowForce()
    {
        cf.force = new Vector3(0f, 0f, -9f);
    }
}