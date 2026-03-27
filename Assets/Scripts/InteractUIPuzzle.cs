using UnityEngine;

public class InteractUIPuzzle : MonoBehaviour
{
    public Renderer[] blocks;   
    public Material[] colors;   
    public int[] answer = { 0, 1, 2 };
    Material[] original;

    bool canInteract = false;
    int i = 0;
    void OnTriggerEnter(Collider other)
    {
        canInteract = true;
    }

    void Start()
    {
        original = new Material[blocks.Length];
        for (int j = 0; j < blocks.Length; j++)
        {
            blocks[j].material = new Material(blocks[j].material);
            original[j] = blocks[j].material;
        }
    }

    void Update()
    {
       
        if (canInteract  && Input.GetKeyDown(KeyCode.E))
        {
            CancelInvoke();
            i = 0;
            Invoke("Next", 0f);
            
        }
    }


    void Next()
    {
        if (i >= answer.Length)
        {
            return;
        }
        blocks[i].material = colors[answer[i]];
        Invoke("Hide", 0.5f);
    }

    void Hide()
    {
        blocks[i].material = original[i];
        i++;
        if (i < answer.Length)
        {
          Invoke("Next", 1f);
        }
    }
}
