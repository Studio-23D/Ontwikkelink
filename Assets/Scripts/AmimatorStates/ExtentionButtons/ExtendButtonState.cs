using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendButtonState : StateMachineBehaviour
{
    private Animator containerAnimator;

    public void SetAnimator(Animator animator) {
        containerAnimator = animator;
    }

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        containerAnimator = containerAnimator ?? animator.gameObject.GetComponent<ExtensionButton>().ExtensionContainerAnimator;
        containerAnimator.SetTrigger("Extend");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        containerAnimator.SetTrigger("Retract");
    }
}
