using UnityEngine;
using System.Collections;

public class PowerPickup : MonoBehaviour
{
    public bool giveGrab = true;
    public bool giveFireball;
    public bool giveTeleport;

    public void OnTriggerEnter(Collider other)
    {
        GauntletController gauntlet = other.gameObject.GetComponent<GauntletController>();
        if (gauntlet)
        {
            if (giveGrab)
            {
                gauntlet.AddPower(other.gameObject.GetComponent<Grab>());
            }

            if (giveFireball)
            {
                gauntlet.AddPower(other.gameObject.GetComponent<FireballShooter>());
            }

            if (giveTeleport)
            {
                gauntlet.AddPower(other.gameObject.GetComponent<Teleporter>());
            }
            Destroy(gameObject);
        }
    }
}
