using UnityEngine;
using System;
using UnityStandardAssets.Characters.FirstPerson;

public class AvatarTeleportTransitionHandler : BaseTransitionHandler {

    public GameObject avatar;
    public GameObject positionToTeleportTo;
    private RigidbodyFirstPersonController controller;

    void Start()
    {
        controller = avatar.GetComponent<RigidbodyFirstPersonController>();
    }

    public override void OnTransitionBegin(Transition transition)
    {
        controller.movementSettings.canControl = false;
    }

    public override void OnWaitingEnd(Transition transition)
    {
        controller.movementSettings.canControl = true;
        avatar.transform.position = positionToTeleportTo.transform.position;
    }
}
