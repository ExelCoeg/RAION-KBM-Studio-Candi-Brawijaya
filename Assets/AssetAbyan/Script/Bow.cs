using System;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bow : MonoBehaviour
{

    public Transform shotPoint;
    public GameObject arrow;
    public GameObject parent;
    public Rigidbody2D arrowRb;
    public Transform enemy;

    [SerializeField] Collider2D objectInRange;

    [SerializeField]LayerMask enemyLayer;
    [SerializeField]float overLapRadius;
    [SerializeField]Status status;

    public Boolean enemyInRange;

    public float timer;
    public float angel;
    public float vo;
    // Start is called before the first frame update
    void Start()
    {
        arrowRb = arrow.GetComponent<Rigidbody2D>();
        parent = transform.parent.gameObject;
    }

    void Update()
    {
        //enemyInRange = parent.GetComponent<NPCAI>().enemyInRange;
        bowRotation();
        CheckSurrounding();
        //Debug.Log(enemy.transform.position);
        if (enemyInRange)
        {
            timer += Time.deltaTime;
            if (timer > 0.5)
            {
                timer = 0;
                Shoot();//nanti ini rencananya bakal kepanggil ketika animasi panahnya selesai
            }
        }
    }
    public void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, transform.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * vo;
    }
    public void bowRotation()
    {
        float angle = calculateAngle();
        if (!float.IsNaN(angle))//cek apakah angle Nan atau tidak(dapat menjadi NaN ketika target berada di luar jarak)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
    public float calculateAngle()
    {
        if (enemyInRange && enemy != null)
        {
            Vector2 direction = enemy.position - transform.position;
            float sinRadianAngle = (10f * arrowRb.gravityScale) * direction.x / (vo * vo);
            float radianAngle2 = Mathf.Asin(sinRadianAngle);
            float angleJustX = (radianAngle2 * Mathf.Rad2Deg) / 2;

            //menambahkan anggle jika sudut y != 0
            float angleRadians = Mathf.Atan2(direction.y, direction.x);
            float angleInDegrees = angleRadians * Mathf.Rad2Deg;

            float angle = angleJustX + angleInDegrees;
            return angle;
        }
        return 0;
    }
    private void CheckSurrounding(){
        objectInRange = Physics2D.OverlapCircle(transform.position, overLapRadius,enemyLayer);
        if (objectInRange != null && objectInRange.tag == "enemy")
        {  
            parent.GetComponent<NPCAI>().Idle(true);
            enemy = objectInRange.transform;
            enemyInRange = true;
        }else{
            parent.GetComponent<NPCAI>().Idle(false);
            enemy = null;
            enemyInRange = false;
        }    
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,overLapRadius);
    }
}
