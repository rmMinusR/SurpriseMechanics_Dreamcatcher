using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_MoveRelToPlayer : StateMachineBehaviour
{
    public float moveSpeed = 0.3f;
    public Vector2 relativeMotion = new Vector2(0, 1);

    private const float rigidbodyMovementAmplifier = 100f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody rb = animator.gameObject.GetComponent<Rigidbody>();

        GameObject player = FindObjectOfType<CharacterControllerIsometric>().gameObject;

        Vector3 targetDelta = player.transform.position - animator.gameObject.transform.position;
        targetDelta.y = 0;
        targetDelta = targetDelta.normalized;

        Vector3 movement = targetDelta * relativeMotion.y + RotateZ90(targetDelta) * relativeMotion.x;

        rb.velocity = movement * moveSpeed * rigidbodyMovementAmplifier;
    }

    private Vector3 RotateZ90(Vector3 v)
    {
        return Vector3.Cross(Vector3.up, v);
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
