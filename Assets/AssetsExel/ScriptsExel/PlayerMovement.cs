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
    bool isSprinting, onTired;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        speed = defaultSpeed;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {

        if(move.x < 0){
            transform.eulerAngles = Vector2.up * 180;
        }
        else{
            transform.eulerAngles = Vector2.zero;
        }
        move.x = Input.GetAxis("Horizontal");
        if(Input.GetKey(KeyCode.LeftShift) && !onTired){ // kalau mencet shift, isSprinting set ke true
            isSprinting = true;
        }
        else{ // kalau ga mencet shift, isSprinting set ke false
            isSprinting = false;
        }
        
        if(energy<=0){
            onTired = true;
        }
        if(onTired){
            onTiredTimer -= Time.deltaTime;
            if(onTiredTimer <= 0){
                onTired = false;
                onTiredTimer = 1f;
            }
        }
        rb.velocity = move * speed * accelStart;
        if(isSprinting){ // kalau ngeshift. maka accelerate
            if(accelStart <  accelLimit) accelStart +=  Time.deltaTime;
            if(energy >= 0 && move.x > 0) energy -= Time.deltaTime;
            
        }
        else{ // kalau ga ngeshift, maka deccelerate
            if(accelStart > 1) accelStart -= Time.deltaTime;
            if(energy <= maxEnergy && !onTired) energy += Time.deltaTime;
        }
        
    }
}
