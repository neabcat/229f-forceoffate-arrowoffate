using UnityEngine;

public class InteractUIPuzzle : MonoBehaviour
{
    public Renderer[] blocks;   
    public Material[] colors;   
    public int[] answer = { 0, 1, 2 };

    bool canInteract = false;
    int i = 0;

    void Update()
    {
       
        if (canInteract  && Input.GetKeyDown(KeyCode.E))
        {
            i = 0;
            Invoke("Next", 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        canInteract = true;
    }

    void Next()
    {
        if (i >= answer.Length)
        {
            return;
        }
        blocks[i].material = colors[answer[i]];
        Invoke("Hide", 1f);
    }

    void Hide()
    {
        blocks[i].material = colors[0];
        i++;
        Invoke("Next", 2f);
    }
}
