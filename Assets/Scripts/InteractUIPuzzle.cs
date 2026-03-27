using System.Collections;
using UnityEngine;

public class InteractUIPuzzle : MonoBehaviour
{
    [Header("Puzzle Blocks")]
    public Renderer[] blocks;              
    public Material[] colors;              
    public int[] answer = { 0, 1, 2 };     

    [Header("Door")]
    public GameObject door;
    public float doorSpeed = 2f;
    public float doorMoveDistance = 2f;

    private Material[] original;
    private int[] currentState;
    private bool isPlaying = false;
    private bool doorOpened = false;

    void Start()
    {
        original = new Material[blocks.Length];
        currentState = new int[blocks.Length];

        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].material = new Material(blocks[i].material);
            original[i] = blocks[i].material;
            currentState[i] = -1; 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPlaying && !doorOpened)
        {
            StartCoroutine(ShowSequence());
        }
    }

    IEnumerator ShowSequence()
    {
        isPlaying = true;

        for (int i = 0; i < answer.Length; i++)
        {
            blocks[i].material = colors[answer[i]];
            yield return new WaitForSeconds(0.5f);

            RestoreBlock(i);
            yield return new WaitForSeconds(0.2f);
        }

        isPlaying = false;
    }

    void RestoreBlock(int index)
    {
        if (currentState[index] == -1)
            blocks[index].material = original[index];
        else
            blocks[index].material = colors[currentState[index]];
    }

    



}
