using UnityEngine;

public class PlatfromMove : MonoBehaviour
{
    float speed = 10;
    public Transform left;
    public Transform right;
    private int direction = 1;
    public bool isPlatfrom = false;
    public bool isUpdown = false;

    // Update is called once per frame
    void Update()
    {
        if (isPlatfrom == true)
        {
            Vector3 pos = transform.position;

            pos.x += direction * speed * Time.deltaTime;

            if (pos.x >= right.position.x)
            {
                pos.x = right.position.x; 
                direction = -1;
            }

            else if (pos.x <= left.position.x)
            {
                pos.x = left.position.x;
                direction = 1;
            }

            transform.position = pos;
        }



        if (isUpdown == true)
        {
            Vector3 pos = transform.position;


            pos.z += direction * speed * Time.deltaTime;

            if (pos.z >= right.position.z)
            {
                pos.z = right.position.z;
                direction = -1;
            }

            else if (pos.z <= left.position.z)
            {
                pos.z = left.position.z;
                direction = 1;
            }

            transform.position = pos;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Target2"))
        {
            isPlatfrom = true;
        }
    }

}
