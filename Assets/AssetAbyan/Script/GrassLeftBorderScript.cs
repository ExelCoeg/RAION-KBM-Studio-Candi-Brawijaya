using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassLeftBorderScript : GrassScript
{
    public GameObject[] borderGrass;
    private void Start() {
        base.SetNextGrass();
    }
    [ContextMenu("call")]
    public override void CallAnim(){
        gameObject.GetComponent<Animator>().Play("GrassLeftChangeEnd");
    }
    [ContextMenu("call Expand Teritory")]
    public void CallEndAnim(){
        gameObject.GetComponent<Animator>().Play("GrassLeftBorderChange");
    }
    public void destroyBorder(){
        foreach (GameObject grass in borderGrass) 
        {
            Destroy(grass);
        }
    }
}
