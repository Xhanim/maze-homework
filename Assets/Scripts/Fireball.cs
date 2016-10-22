using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public int damage = 1;

    void OnTriggerEnter(Collider collider)
    {
        if (!collider.isTrigger)
        {
            Health health = collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
