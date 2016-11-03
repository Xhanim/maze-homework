using UnityEngine;
using System.Collections;

public abstract class BaseTransitionHandler : MonoBehaviour {

    public virtual void OnTransitionBegin(Transition transition)
    {

    }

    public virtual void OnWaitingBegin(Transition transition)
    {

    }

    public virtual void OnWaitingEnd(Transition transition)
    {

    }

    public virtual void OnTransitionEnd(Transition transition)
    {

    }
}
