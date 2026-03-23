using UnityEngine;

public class AcherTarget : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float acceleration;

    public GameObject weight;

    void CalculateForce()
    {
        Rigidbody rb = weight.GetComponent<Rigidbody>();
        float  mass = rb.mass;
        force = mass * acceleration ;
        rb.AddForce(new Vector3 (0, force, force));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test");

        if (gameObject.CompareTag("Taget1"))
        {
            acceleration = 300f;
            CalculateForce();
            Debug.Log("1");
        }
        else if (gameObject.CompareTag("Target2"))
        {
            acceleration = 400f;
            CalculateForce();
            Debug.Log("2");
        }
        else if (gameObject.CompareTag("Target3"))
        {
            acceleration = 500f;
            CalculateForce();
            Debug.Log("3");
        }
    }
}
