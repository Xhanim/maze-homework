using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GauntletController : MonoBehaviour {
    private List<MonoBehaviour> powers;

    // 0 = grab, 1 = fireball or 2 = teleport
    private int currentPower;
    
	// Use this for initialization
	void Start () {
        powers = new List<MonoBehaviour>();
        powers.Add(GetComponent<Grab>());
        powers.Add(GetComponent<Fireball>());
        powers.Add(GetComponent<Teleporter>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) //  left
        {
            Previous();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0) // right
        {
            Next();
        }
    }

    private void Previous()
    {
        currentPower--;
        // loop to the right
        if (currentPower < 0)
        {
            currentPower = 2;
        }
        UpdatePower();
    }

    private void Next()
    {
        currentPower++;
        // loop to the left
        if (currentPower > 2)
        {
            currentPower = 0;
        }
        UpdatePower();
    }

    private void UpdatePower()
    {
        for (int i = 0; i < powers.Count; i++)
        {
            // same index? set as enabled and disable the rest
            if (i == currentPower)
            {
                powers[i].enabled = true;
            }else
            {
                powers[i].enabled = false;
            }
        }
    }

    public ITargetAnalyzer GetTargetAnalyzer()
    {
        return (ITargetAnalyzer) powers[currentPower];
    }
}
