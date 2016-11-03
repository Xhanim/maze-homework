using UnityEngine;
using System.Collections;

public class TransitionStateMachine : StateMachineBehaviour {

    public float completionPercentage;
    private bool transitionActivated;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!transitionActivated && stateInfo.normalizedTime >= completionPercentage)
        {
            transitionActivated = true;
            animator.SetTrigger("transition");
        }
    }
}
