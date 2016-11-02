using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShootableSwitch : MonoBehaviour, Health {
    public bool remainActive = true;
    private bool isActive;
    public List<BaseActivator> activators;

    public void TakeDamage(GameObject origin, int damage)
    {
        if (!remainActive || !isActive)
        {
            foreach (BaseActivator activator in activators)
            {
                activator.Activate(gameObject);
            }
            isActive = true;
        }
    }
}
