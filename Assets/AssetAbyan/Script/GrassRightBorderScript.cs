using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassRightBorderScript : GrassScript
{
    public GameObject[] borderGrass;
    private void Start() {
        base.SetNextGrass();
    }
    [ContextMenu("call")]
    public override void CallAnim(){
        gameObject.GetComponent<Animator>().Play("GrassRightChangeEnd");
    }
    [ContextMenu("call Expand Teritory")]
    public override void CallEndAnim(){
        gameObject.GetComponent<Animator>().Play("GrassRightBorderChange");
    }
    public void destroyBorder(){
        foreach (GameObject grass in borderGrass) 
        {
            Destroy(grass);
        }
    }
}
