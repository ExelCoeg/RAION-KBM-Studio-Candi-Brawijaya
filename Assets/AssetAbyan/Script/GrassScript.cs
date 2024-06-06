using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassScript : MonoBehaviour
{
    public Collider2D nextGrass;
    public float offsite;
    // Start is called before the first frame update
    void Start()
    {
        nextGrass = Physics2D.OverlapCircle(transform.position + new Vector3(offsite,0,0),0.2f,LayerMask.GetMask("Ground"));
    }
    public void callNextAnim(){
        if(nextGrass != null){
            nextGrass.gameObject.GetComponent<GrassScript>().callAnim();
        }
    }
    [ContextMenu("call")]
    public void callAnim(){
        if (gameObject.name == "GreasLeftGroup"){}
            {
                gameObject.GetComponent<Animator>().Play("GrassLeftChange");
            }
            if (name == "GreasRightGroup"){}
            {
                gameObject.GetComponent<Animator>().Play("GrassRightChange");
            }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position + new Vector3(offsite,0,0), 0.2f);
    }
}
