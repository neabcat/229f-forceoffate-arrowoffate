using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Swicth"))
        {
            Destroy(door.gameObject);
        }

    }


}
