using UnityEngine;
using System.Collections;
using System;

public class RelicActivator : BaseActivator {

    private AvatarTeleportTransition teleportTransition;

    void Start()
    {
        teleportTransition = GetComponent<AvatarTeleportTransition>();
    }

    public override void Activate(GameObject trigger)
    {
        teleportTransition.enabled = true;
    }

    public override void Desactivate()
    {
        teleportTransition.enabled = false;
    }
}
