using UnityEngine;

public class AcherTarget : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float mass;
    [SerializeField] float acceleration;

    void CalculateForce()
    {
        mass = GetComponent<Rigidbody>().mass;
        acceleration = force / mass;
        GetComponent<Rigidbody>().AddForce(force, force, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Taget1"))
        {
            force = 250f;
            CalculateForce();
        }
        else if (collision.gameObject.CompareTag("Target2"))
        {
            force = 300f;
            CalculateForce();
        }
        else if (collision.gameObject.CompareTag("Target3"))
        {
            force = 350f;
            CalculateForce();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
