using UnityEngine;
using System.Collections;

public class EnemyFireball : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spawn;
    public float cooldown = 3;
    private float currentCooldown;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        currentCooldown += Time.fixedDeltaTime;
        if (currentCooldown >= cooldown)
        {
            currentCooldown = 0;
            GameObject ballInstance = Instantiate(prefab, spawn.transform.position, spawn.transform.rotation) as GameObject;
            ballInstance.GetComponent<Rigidbody>().AddForce(spawn.transform.forward * 15, ForceMode.Impulse);
        }
    }
}
