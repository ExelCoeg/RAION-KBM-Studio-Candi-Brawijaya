using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed & Acceleration")]
    [SerializeField] float defaultSpeed = 5f;
    [SerializeField] float accelLimit;
    
    float accelStart = 1f;
    float speed;
    Rigidbody2D rb;
    Vector2 move;
    bool isSprinting;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        speed = defaultSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        if(Input.GetKey(KeyCode.LeftShift)){ // kalau mencet shift, isSprinting set ke true
            isSprinting = true;
        }
        else{ // kalau ga mencet shift, isSprinting set ke false
            isSprinting = false;
        }
        rb.velocity = move * speed * accelStart;
        if(isSprinting){ // kalau ngeshift. maka accelerate
            if(accelStart <  accelLimit) accelStart +=  Time.deltaTime;
        }
        else{ // kalau ga ngeshift, maka deccelerate
            if(accelStart > 1) accelStart -= Time.deltaTime;
        }
    }
}
