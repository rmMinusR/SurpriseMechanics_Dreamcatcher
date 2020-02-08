using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Attack : StateMachineBehaviour
{
    public GameObject attackPrototype;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackPrototype != null)
        {
            GameObject o = Instantiate(attackPrototype, FindObjectOfType<PlayerCombatController>().attackParent);
            o.transform.rotation = FindObjectOfType<PlayerCombatController>().attackRotation.rotation * o.transform.rotation;
            o.transform.position =  o.transform.right     * attackPrototype.transform.position.x +
                                    o.transform.up        * attackPrototype.transform.position.y +
                                    o.transform.forward   * attackPrototype.transform.position.z +
                                    FindObjectOfType<PlayerCombatController>().attackParent.position;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
