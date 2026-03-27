using System.Collections;
using UnityEngine;

public class Minus : MonoBehaviour
{
    public float force;
    public float spin;
    public float magnus;
    public bool isTake = false;
    public float targetY = -35.6f;
    float speed = 1;

    Rigidbody rb;
    public GameObject lift; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTake)
        {
            Magnus();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Magnus");

            rb.isKinematic = true;
            StartCoroutine(MoveUp());
        }

        else if (gameObject.CompareTag("Target2"))
        {
            Debug.Log("-*-");
            isTake = true;

        }
    }

    void Magnus()
    {
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        rb.AddTorque(Vector3.up * spin);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        Vector3 spin = rb.angularVelocity;

        Vector3 magnusForce = magnus * Vector3.Cross(velocity, spin);

        rb.AddForce(magnusForce);
    }

    IEnumerator MoveUp()
    {
        Vector3 startPos = lift.transform.position;
        Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            lift.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        lift.transform.position = endPos;
    }
}
