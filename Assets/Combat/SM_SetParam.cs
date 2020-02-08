using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_SetParam : StateMachineBehaviour
{
    public string[] paramName;
    public string[] paramValue;

    private void TrySetVars(Animator animator) {
        //Debug.Log("Attempting to execute");
        for(int i = 0; i < paramName.Length && i < paramValue.Length; i++)
        {
            //Debug.Log("Attempt #" + i + ": "+paramName[i]+" := "+paramValue[i]);
            bool b_parsed;
            int i_parsed;
            float f_parsed;
                 if (bool .TryParse(paramValue[i], out b_parsed)) { animator.SetBool   (paramName[i], b_parsed); }
            else if (int  .TryParse(paramValue[i], out i_parsed)) { animator.SetInteger(paramName[i], i_parsed); }
            else if (float.TryParse(paramValue[i], out f_parsed)) { animator.SetFloat  (paramName[i], f_parsed); }
            else                                                    animator.SetTrigger(paramName[i]);
            
        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TrySetVars(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TrySetVars(animator);
    }

    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //} 

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
