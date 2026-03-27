using UnityEngine;

public class stairs : MonoBehaviour
{
    public GameObject stairs2;
    public GameObject stairs3;
    public GameObject stairs4;
    public GameObject stairs5;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("PlatFrom"))
        {
            stairs2.SetActive(true);

        }

        else if (gameObject.CompareTag("PlatFrom1"))
        {
            stairs3.SetActive(true);
        }

        else if (gameObject.CompareTag("PlatFrom2"))
        {
            stairs4.SetActive(true);
        }

        else if ((gameObject.CompareTag("Lift")))
        {
            stairs5.SetActive(true);
        }
    }
}
