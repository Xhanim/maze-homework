using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class ShootableSwitch : MonoBehaviour, Health
{
    public bool remainActive = true;
    private bool isActive;
    public List<BaseActivator> activators;
    // rotation counter
    private float counter;

    public void FixedUpdate()
    {
        if (isActive && !remainActive)
        {
            counter += Time.fixedDeltaTime;
            if(counter >= 3)
            {
                GetComponent<AutoMoveAndRotate>().enabled = false;
                counter = 0;
            }
        }
    }

    public void TakeDamage(GameObject origin, int damage)
    {
        if (!remainActive || !isActive)
        {
            foreach (BaseActivator activator in activators)
            {
                activator.Activate(gameObject);
            }
            isActive = true;
            GetComponent<AutoMoveAndRotate>().enabled = true;
        }
    }
}
