using UnityEngine;

public class AcherTarget : MonoBehaviour
{
    float force;
    float acceleration;
    [SerializeField] float forceSpin;

    public GameObject weight;
    public GameObject Wall;
    public GameObject rock;

    private void Start()
    {
        forceSpin = 0f;
        force = 0f;
        acceleration = 0f;
    }

    void CalculateForce()
    {
        Rigidbody rb = weight.GetComponent<Rigidbody>();
        float mass = rb.mass;
        force = mass * acceleration;
        rb.AddForce(new Vector3(force, force, 0));
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
            forceSpin = 6000;
            Rigidbody rb = weight.GetComponent<Rigidbody>();
            rb.AddTorque(Vector3.up * forceSpin);

            Debug.Log("2");
        }
        else if (gameObject.CompareTag("Target3"))
        {
            acceleration = 600f;
            CalculateForce();
            Debug.Log("3");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Rock"))
        {
            Debug.Log("Can");
            force = 20;
            Rigidbody rb = rock.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.down * force);

            Invoke("StopRock", 5f);
        }
        
    }

    private void StopRock()
    {
        Rigidbody rb = rock.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

}
