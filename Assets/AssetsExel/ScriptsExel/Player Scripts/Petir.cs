
using UnityEngine;

public class Petir : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float offsetCircleX;
    public float offsetCircleY;
    public float petirRadius;
    private Vector2 circlePos;
    
    private bool strike = true;
    private void Start() {
        circlePos = new Vector2(transform.position.x - offsetCircleX, transform.position.y - offsetCircleY);
    }
    // Update is called once per frame
    void Update()
    {
        if(strike){
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(circlePos, petirRadius, enemyLayer);
            foreach(Collider2D enemy in hitEnemies){
                enemy.GetComponent<IDamagable>().TakeDamage(25);
            }
            strike = false;
        }
    }
   
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circlePos, petirRadius);
    }
}
