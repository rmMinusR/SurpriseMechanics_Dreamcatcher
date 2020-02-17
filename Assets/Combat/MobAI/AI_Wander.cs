using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Wander : StateMachineBehaviour
{
    private Vector3 wanderDirection;
    public float wanderSpeed = 0.3f;
    public float wanderDistance = 2f;

    private const float rigidbodyMovementAmplifier = 100f;

    private float nextWander;
    public float wanderDelay = 2f;

    public bool isAnchored = false;
    private Vector3 anchor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isAnchored && anchor == null) anchor = animator.gameObject.transform.position;
        Reset(animator.gameObject);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > nextWander) {

            Vector3 wander = wanderDirection.normalized * wanderSpeed * Time.deltaTime;
            
            animator.gameObject.GetComponent<Rigidbody>().velocity = wander * rigidbodyMovementAmplifier;

            if (wander.magnitude >= wanderDirection.magnitude)
            {
                Reset(animator.gameObject);
            }
            else
            {
                wanderDirection -= wander;
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Reset(animator.gameObject);
    }

    private void Reset(GameObject target)
    {
        nextWander = Time.time + wanderDelay;

        if (isAnchored)
        {
            Vector3 wanderTarget = anchor + Random.onUnitSphere;
            wanderDirection = wanderTarget - target.transform.position;
            wanderDirection.y = 0;
        }
        else
        {
            wanderDirection = Random.onUnitSphere;
            wanderDirection.y = 0;
            wanderDirection = wanderDirection.normalized * wanderDistance;
        }
    }

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
