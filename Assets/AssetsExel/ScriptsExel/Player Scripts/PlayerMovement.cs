using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float energy;
    [SerializeField] float maxEnergy = 3f;
    

    [Header("Speed & Acceleration")]
    [SerializeField] float defaultSpeed = 5f;
    [SerializeField] float accelLimit;
    
    float accelStart = 1f;
    float speed;
    float onTiredTimer = 1f;
    Rigidbody2D rb;
    Vector2 move;
    bool isSprinting, onTired, isFacingRight = true;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        speed = defaultSpeed;
        energy = maxEnergy;
    }
    /*
    1. shift -> makan energy
    2. energy habis -> player cape
    3. energy penuh -> player bisa sprint lagi
    
    */
    // Update is called once per frame
    void Update()
    {

        move.x = Input.GetAxis("Horizontal");
        if(move.x < 0 && isFacingRight){
            transform.eulerAngles = Vector2.up * 180;
            isFacingRight = false;
        }
        if(move.x > 0 && !isFacingRight){
            transform.eulerAngles = Vector2.zero;
            isFacingRight = true;
        }
        if(Input.GetKey(KeyCode.LeftShift) && !onTired){ // kalau mencet shift, isSprinting set ke true
            isSprinting = true;
        }
       
        rb.velocity = move * speed * accelStart;

        
        if(energy<=0){
            onTired = true;
        }
        if(onTired){
            isSprinting = false;
            onTiredTimer -= Time.deltaTime;
            if(onTiredTimer <= 0){
                onTired = false;
                onTiredTimer = 1f;
            }
        }
        if(isSprinting){ // kalau ngeshift. maka accelerate
            if(accelStart <  accelLimit) accelStart +=  Time.deltaTime;
            if(energy >= 0 && move.x != 0) energy -= Time.deltaTime;
            
        }
        else{ // kalau ga ngeshift, maka deccelerate
            if(accelStart > 1) accelStart -= Time.deltaTime;
            if(energy <= maxEnergy && !onTired) energy += Time.deltaTime;
        }
        
    }
}
