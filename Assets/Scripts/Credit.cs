using UnityEngine;

public class Credit : MonoBehaviour
{
    public RollingText UIcredit;
    public GameObject UI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        UI.SetActive(true);
        UIcredit.Rolling();
    }
}
