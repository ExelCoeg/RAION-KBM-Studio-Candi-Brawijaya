
using UnityEngine;

public abstract class GrassScript : MonoBehaviour
{
    public Collider2D nextGrass;
    public Collider2D nextGrassBorder;
    public float offsite;
    public void CallNextAnim()
    {
        
        if (nextGrass != null)
        {
            nextGrass.gameObject.GetComponent<GrassScript>().CallAnim();
        }
        else{
            nextGrassBorder = Physics2D.OverlapCircle(transform.position + new Vector3(offsite, 0, 0), 0.3f, LayerMask.GetMask("GrassBorder"));
            if(nextGrassBorder != null){
                nextGrassBorder.gameObject.GetComponent<GrassScript>().CallAnim();
            }
        }
        
    }
    
    public abstract void CallAnim();
    public virtual void CallEndAnim(){}
    public void SetNextGrass(){
        nextGrass = Physics2D.OverlapCircle(transform.position + new Vector3(offsite, 0, 0), 0.2f, LayerMask.GetMask("Ground"));
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(offsite, 0, 0), 0.2f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(offsite, 0, 0), 0.3f);
    }
}
