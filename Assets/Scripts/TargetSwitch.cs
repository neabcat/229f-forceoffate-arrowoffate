using UnityEngine;

public class TargetSwitch : MonoBehaviour
{
    public Collider floorCollider;
    public Renderer floorRenderer;
    public Material newVisualMaterial;
    public PhysicsMaterial newPhysicMaterial;
    public GameObject cube;
    public GameObject SpawnCubePrefab;

    Vector3 pos;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Physic target 1"))
        {
            floorCollider.sharedMaterial = newPhysicMaterial;
            floorRenderer.material = newVisualMaterial;
            pos = cube.transform.position;
            Destroy(cube.gameObject);

            Invoke("SpawnCube", 10f);
        }
    }
    void SpawnCube()
    {
        cube = Instantiate(SpawnCubePrefab, pos, Quaternion.identity);
    }
}
