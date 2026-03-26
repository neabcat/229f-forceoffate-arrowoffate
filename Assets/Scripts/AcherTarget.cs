using UnityEngine;

public class AcherTarget : MonoBehaviour
{
    float force;
    float acceleration;
    [SerializeField] float forceSpin;

    public GameObject weight;
    public GameObject Wall;

    void CalculateForce()
    {
        Rigidbody rb = weight.GetComponent<Rigidbody>();
        float mass = rb.mass;
        force = mass * acceleration;
        rb.AddForce(new Vector3(0, force, force));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test");

        if (gameObject.CompareTag("Taget1"))
        {
            weight.GetComponent<Renderer>().material.color = Color.blue;
            Debug.Log("1");
        }
        else if (gameObject.CompareTag("Target2"))
        {
            Rigidbody rb = weight.GetComponent<Rigidbody>();
            rb.AddTorque(Vector3.up * forceSpin);
            Debug.Log("2");
        }
        else if (gameObject.CompareTag("Target3"))
        {
            acceleration = 600f;
            CalculateForce();

            Destroy(Wall.gameObject, 10f);
            Debug.Log("3");
        }
    }

}
