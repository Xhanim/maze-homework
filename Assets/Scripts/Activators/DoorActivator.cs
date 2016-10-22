using UnityEngine;
using System.Collections;
using System;

public class DoorActivator : BaseActivator {
    /**
     * Minimal activations to actually activate the door animation.
     * ie: two switches with this door attached and both need to be active before the animation triggers.
     * */
    public int activations = 1;
    private int currentActivations;
    private Animator animator;
    private bool isActive;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Activate(GameObject trigger)
    {
        CheckActivation(1);
    }

    public override void Desactivate()
    {
        CheckActivation(-1);
    }

    private void CheckActivation(int count)
    {
        currentActivations = Mathf.Clamp(currentActivations + count, 0, activations);
        if (currentActivations >= activations)
        {
            animator.SetBool("IsOpen", true);
        }else
        {
            animator.SetBool("IsOpen", false);
        }
    }
}
