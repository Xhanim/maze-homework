using UnityEngine;
using System.Collections;
using System;

public class DefaultEnemyHealth : AbstractHealth {

    protected override void OnHealthDepleted()
    {
        Destroy(gameObject);
    }
}
