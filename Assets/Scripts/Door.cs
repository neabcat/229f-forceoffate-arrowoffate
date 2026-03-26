using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public float speed = 2f;
    private bool open = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Swicth"))
        {
            open = true;
        }
    }

    void Update()
    {
       if (open) 
       {
            door.position += Vector3.down * speed * Time.deltaTime;
       } 
    }


}
