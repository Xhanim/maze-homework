using UnityEngine;
using System;

public class AvatarTeleportTransition : BaseTransition {

    public GameObject avatar;
    public GameObject positionToTeleportTo;

    public override void OnTransitionBegin()
    {

    }

    public override void OnWaitingEnd()
    {
        avatar.transform.position = positionToTeleportTo.transform.position;
    }
}
