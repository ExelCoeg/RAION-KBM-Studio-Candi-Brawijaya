using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassRightScript : GrassScript
{
    private void Start() {
        base.SetNextGrass();
    }
    [ContextMenu("call")]
    public override void CallAnim(){
        gameObject.GetComponent<Animator>().Play("GrassRightChange");
    }
}
