using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GauntletController : MonoBehaviour {

    public GameObject gauntletModel;
    public Material[] powerMaterial;
    public GameObject powerMaterialHolder;
    private List<MonoBehaviour> powers;
    // 0 = grab, 1 = fireball or 2 = teleport
    private int currentPower;
    
	// Use this for initialization
	void Start () {
        powers = new List<MonoBehaviour>();
        powers.Add(GetComponent<Grab>());
        powers.Add(GetComponent<FireballShooter>());
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
            currentPower = powers.Count - 1;
        }
        UpdatePower();
    }

    private void Next()
    {
        currentPower++;
        // loop to the left
        if (currentPower > powers.Count - 1)
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
                powerMaterialHolder.GetComponent<Renderer>().material = powerMaterial[i];
            }else
            {
                powers[i].enabled = false;
            }
        }
    }

    public ITargetAnalyzer GetTargetAnalyzer()
    {
        return powers.Count > 0 ? (ITargetAnalyzer) powers[currentPower] : null;
    }

    public void AddPower(MonoBehaviour power)
    {
        powers.Add(power);
        currentPower = powers.Count-1;
        UpdatePower();
    }
}
