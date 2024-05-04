
using UnityEngine;

public class Petir : MonoBehaviour
{
    public LayerMask enemyLayer;
    // Update is called once per frame
    void Update()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 1f, enemyLayer);

        // foreach (Collider2D enemy in enemies)
        // {
        //     // enemy.GetComponent<NPCAI>().TakeDamage(1);
        // }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
