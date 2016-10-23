using UnityEngine;
using System.Collections;
using System;

public class MovePatternActivator : BaseActivator {

    public override void Activate(GameObject trigger)
    {
        GetComponent<MovePattern>().enabled = true;
    }

    public override void Desactivate()
    {
        GetComponent<MovePattern>().enabled = false;
    }
}
