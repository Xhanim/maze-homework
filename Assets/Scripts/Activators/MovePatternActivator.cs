using UnityEngine;
using System.Collections;
using System;

public class MovePatternActivator : BaseActivator {

    // number of calls from activators to change state
    public int activations = 1;
    // a activatior can be called by different objects at the same time!
    private int currentActivations;

    public override void Activate(GameObject trigger)
    {
        currentActivations++;
        if (currentActivations >= activations)
        {
            GetComponent<MovePattern>().enabled = true;
        }
    }

    public override void Desactivate()
    {
        currentActivations--;
        if (currentActivations < activations)
        {
            GetComponent<MovePattern>().enabled = false;
        }
    }
}
