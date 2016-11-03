using UnityEngine;
using System.Collections;

public class GameObjectActivatorTransitionHandler : BaseTransitionHandler
{
    public GameObjectActivator activator;

    public override void OnWaitingBegin(Transition transition)
    {
        activator.Activate(gameObject);
    }
}
