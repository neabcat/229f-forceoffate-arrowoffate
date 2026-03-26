using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetSwitch : MonoBehaviour
{
    public Collider floorCollider;
    public Renderer floorRenderer;
    public Material newVisualMaterial;
    public PhysicsMaterial newPhysicMaterial;
    public GameObject cube;
    public GameObject way;
    public Player Player;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            floorCollider.sharedMaterial = newPhysicMaterial;
            floorRenderer.material = newVisualMaterial;
            Destroy(cube.gameObject);
        }

        if (collision.gameObject.CompareTag("Taget1"))
        {
            floorCollider.sharedMaterial = newPhysicMaterial;
            floorRenderer.material = newVisualMaterial;
        }

        if ((collision.gameObject.CompareTag("Target2")))
        {
            floorCollider.sharedMaterial = newPhysicMaterial;
            floorRenderer.material = newVisualMaterial;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
