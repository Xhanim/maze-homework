using UnityEngine;
using System.Collections;

public class PlayerHealth : AbstractHealth
{
    public GameObject checkpoint;

    protected override void OnHealthDepleted()
    {
        Vector3 newPosition = checkpoint.transform.position;
        newPosition.y += 1;
        transform.position = newPosition;
        currentHealth = initialHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkpoint = other.gameObject;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Void"))
        {
            TakeDamage(gameObject, 100);
        }
    }
}
