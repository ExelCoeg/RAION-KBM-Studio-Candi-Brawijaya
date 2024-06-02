// Animates the position in an arc between sunrise and sunset.

using UnityEngine;
using System;

public class ArrowScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float vo;
    public float damage;
    public Boolean isHit;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * vo;
        Invoke("Destroy",5f);
    }

    void Update()
    {
        if (!isHit){
            float angle = Mathf.Atan2(rb.velocity.y,rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //Debug.Log(transform.rotation.eulerAngles.z);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Enemy")){
            Destroy(collision.gameObject);
            //collision.gameObject.GetComponent<IDamagable>().TakeDamage((int)damage);
        }else if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Ground");
            isHit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Invoke("Destroy",1f);
        }
    }
    public void Destroy(){
        Destroy(gameObject);
    }
}
