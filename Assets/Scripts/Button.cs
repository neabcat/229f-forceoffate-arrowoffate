using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject ballPrefab;  
    public Transform spawnPoint;

    public float spawnForce = 5f;   


     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SpawnBall();
        }

    }

    void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.forward * spawnForce, ForceMode.Impulse);
        }
    }

}
