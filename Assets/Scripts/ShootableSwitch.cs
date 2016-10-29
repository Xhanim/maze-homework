using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShootableSwitch : MonoBehaviour, Health {

    public List<BaseActivator> activators;

    public void TakeDamage(GameObject origin, int damage)
    {
        foreach (BaseActivator activator in activators)
        {
            activator.Activate(gameObject);
        }
    }
}
