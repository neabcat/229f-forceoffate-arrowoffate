using UnityEngine;

public class DeadlyWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // เช็ค Player
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.Die();
            return;
        }

        // เช็ค Slime
        SlimeController slime = other.GetComponent<SlimeController>();
        if (slime != null)
        {
            Destroy(slime.gameObject);
            return;
        }

        // สิ่งมีชีวิตอื่นๆในอนาคต (เช่น enemy ใหม่)
        var dieMethod = other.GetComponent<MonoBehaviour>();

        if (dieMethod != null)
        {
            // fallback เผื่อไม่มี Die() ก็ลบทิ้ง
            Destroy(other.gameObject);
        }
    }
}