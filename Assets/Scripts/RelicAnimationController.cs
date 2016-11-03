using UnityEngine;
using System.Collections;

public class RelicAnimationController : MonoBehaviour {

    public Transition transition;
    public Animator animator;
    private bool transitionActivated;
    
	void Update () {
	    if (!transitionActivated && animator.GetBool("transition"))
        {
            transitionActivated = true;
            transition.StartTransition();
        }
	}
}
