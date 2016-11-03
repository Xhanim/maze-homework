using UnityEngine;
using System.Collections;
using System;

public class TransitionActivator : BaseActivator {

    public Transition transition;

    public override void Activate(GameObject trigger)
    {
        transition.StartTransition();
    }

    public override void Desactivate()
    {
    }
}
