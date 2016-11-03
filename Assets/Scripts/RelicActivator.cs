using UnityEngine;
using System.Collections;
using System;

public class RelicActivator : BaseActivator {

    private Transition teleportTransition;

    void Start()
    {
        teleportTransition = GetComponent<Transition>();
    }

    public override void Activate(GameObject trigger)
    {
        teleportTransition.StartTransition();
    }

    public override void Desactivate()
    {
    }
}
