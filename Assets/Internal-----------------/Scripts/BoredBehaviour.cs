using EasyUIAnimator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoredBehaviour : StateMachineBehaviour
{
    [SerializeField]private float timeUntilBored;
    [SerializeField] private int numberOfBoredAnimations;
    private bool isBored;
    private float idleTime;
    private int boredAnim;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isBored) 
        {
            idleTime += Time.deltaTime;
            if(idleTime > timeUntilBored)
            {
                isBored = true;
                int boredAnim = Random.Range(1, numberOfBoredAnimations + 1);
                animator.SetFloat("BoredAnimation", boredAnim);
            }
        }else if(stateInfo.normalizedTime % 1 > .98)
        {
            ResetIdle(animator);
        }
        //animator.SetFloat("BoredAnimation", boredAnim, .2f, Time.deltaTime);
    }

    private void ResetIdle(Animator animator)
    {
        if (isBored) { boredAnim--; }
        isBored = false;
        idleTime = 0;
        boredAnim = 0;

        animator.SetFloat("BoredAnimation", 0);
    }

   
}
