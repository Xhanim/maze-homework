using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour {
    public string text;
    public float time;
    public Text label;
    private float currentTime;
    private bool active;

    void FixedUpdate()
    {
        if (active)
        {
            currentTime += Time.fixedDeltaTime;
            if(currentTime >= time)
            {
                label.text = "";
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Avatar")
        {
            label.text = text;
            active = true;
        }
    }
}
