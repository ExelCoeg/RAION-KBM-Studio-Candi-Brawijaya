
using UnityEngine;

public class AfterLightningStrike : StateMachineBehaviour
{
   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.gameObject.GetComponent<Keris>().Petir(new Vector3(animator.gameObject.GetComponent<Keris>().getMousePosX() + animator.gameObject.GetComponent<Keris>().petirOffsetX, animator.gameObject.GetComponent<Keris>().petirOffsetY,0));
    }

  
}
