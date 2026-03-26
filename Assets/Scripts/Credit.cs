using UnityEngine;

public class Credit : MonoBehaviour
{
    public RollingText UIcredit;
    public GameObject UI;


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        UI.SetActive(true);
        UIcredit.Rolling();
    }
}
