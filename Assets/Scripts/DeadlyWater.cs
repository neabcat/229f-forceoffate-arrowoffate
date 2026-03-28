using UnityEngine;

public class DeadlyWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.Die();
            return;
        }

        SlimeController slime = other.GetComponent<SlimeController>();
        if (slime != null)
        {
            Destroy(slime.gameObject);
            return;
        }

        var dieMethod = other.GetComponent<MonoBehaviour>();

        if (dieMethod != null)
        {
            Destroy(other.gameObject);
        }
    }
}