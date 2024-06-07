using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class GrassScript : MonoBehaviour
{
    public Collider2D nextGrass;
    public float offsite;
    public void CallNextAnim()
    {
        if (nextGrass != null)
        {
            nextGrass.gameObject.GetComponent<GrassScript>().CallAnim();
        }
    }
    
    public abstract void CallAnim();
    
    public void SetNextGrass(){
        nextGrass = Physics2D.OverlapCircle(transform.position + new Vector3(offsite, 0, 0), 0.2f, LayerMask.GetMask("Ground"));
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(offsite, 0, 0), 0.2f);
    }
}
