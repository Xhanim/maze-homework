using UnityEngine;
using System.Collections;

public class PlayerHealth : AbstractHealth
{
    public Transform checkpoint;

    protected override void OnHealthDepleted()
    {
        Vector3 newPosition = checkpoint.position;
        newPosition.y += 1;
        transform.position = newPosition;
        currentHealth = initialHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkpoint = other.gameObject.transform.FindChild("Respawn");
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
