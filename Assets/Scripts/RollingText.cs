using UnityEngine;

public class RollingText : MonoBehaviour
{

    public float scollSpeed = 40f;
    private RectTransform rectTransform;
    private bool isRolling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRolling)
        {
            rectTransform.anchoredPosition += new Vector2(0, scollSpeed * Time.deltaTime);
        }
    }

    public void Rolling()
    {
        isRolling = true;   
    }
}
