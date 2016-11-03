using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyActivator : MonoBehaviour
{
    public List<GameObject> turrets;

    public void OnTriggerEnter(Collider other)
    {
        foreach (GameObject turret in turrets)
        {
            turret.GetComponent<EnemyFireball>().enabled = true;
        }
        Destroy(gameObject);
    }
}
