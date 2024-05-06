using System;
using UnityEngine;
using UnityEngine.Animations;

public class Bow : MonoBehaviour
{

    public Transform shootPoint;
    public GameObject arrow;
    public GameObject parent;
    public Rigidbody2D arrowRb;
    public Transform enemy;

    [SerializeField] Collider2D objectInRange;
    [SerializeField] Animator animator;
    [SerializeField]LayerMask enemyLayer;
    [SerializeField]float overLapRadius;
    [SerializeField]Status status;

    public Boolean enemyInRange;

    public float timer;
    public float angle;
    public float vo;
    // Start is called before the first frame update
    private void Awake() {
        arrowRb = arrow.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        //parent = GetComponentInParent<GameObject>();
        animator = parent.GetComponent<NPCAI>().animator;
        ChangeDistance(overLapRadius);
    }

    void Update()
    {
        CalculatePower();
        //enemyInRange = parent.GetComponent<NPCAI>().enemyInRange;
        BowRotation();
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
        GameObject newArrow = Instantiate(arrow, shootPoint.position, transform.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * vo;
    }
    public void BowRotation()
    {
        angle = CalculateAngle();
        if (!float.IsNaN(angle))//cek apakah angle Nan atau tidak(dapat menjadi NaN ketika target berada di luar jarak)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
            animator.SetFloat("aimingDegree", angle);
        }
    }
    public float CalculateAngle()
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
    public float CalculatePower(){
        //overLapRadius = ((vo*vo) * Mathf.Sin(90))/(10 * arrowRb.gravityScale);
        return Mathf.Sqrt(overLapRadius * 10f * arrowRb.gravityScale);
    }
    public void ChangeDistance(float distance){
        overLapRadius = distance;
        vo = CalculatePower();
        overLapRadius -=2;//dikurangi untuk memastikan musuh berada pada jarak tembak
        parent.GetComponent<NPCAI>().overLapDetected = overLapRadius;
    }
    private void CheckSurrounding(){
        objectInRange = Physics2D.OverlapCircle(transform.position, overLapRadius,enemyLayer);
        if (objectInRange != null && objectInRange.tag == "Enemy")
        {  
            parent.GetComponent<NPCAI>().Idle(true);

            enemy = objectInRange.transform;
            enemyInRange = true;
        }else if (enemyInRange){
            parent.GetComponent<NPCAI>().Idle(false);
            
            enemy = null;
            enemyInRange = false;
        }    
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,overLapRadius);
    }
}
