using UnityEngine;
using System.Collections;
using System;

/**
 * Moves game object up and down with a constant speed and a y limit.
 * */
public class UpDownActivator : BaseActivator
{
    public float speed = 3;
    public float yLimit;
    public float swiftDelay = 4;
    // remain in the Y limit
    public bool fixOnGoal = false;
    // number of calls from activators to change state
    public int activations = 1;
    private float initialY;
    private bool active;
    private int direction = 1;
    // used when it's desactivated and it should go down to return to the initial position
    private bool reseting;
    private bool waitingSwift;
    private float swiftCounter;
    // keep track of how many activators called this
    // a activatior can be called by different objects at the same time!
    private int currentActivations;

    // Use this for initialization
    void Start()
    {
        initialY = gameObject.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (waitingSwift)
        {
            swiftCounter += Time.deltaTime;
            if (swiftCounter >= swiftDelay)
            {
                waitingSwift = false;
                swiftCounter = 0;
            }
            return;
        }

        if (active || reseting)
        {
            // check Y limits
            if (gameObject.transform.localPosition.y >= yLimit)
            {
                if (fixOnGoal && !reseting) return;
                direction = -1;
                waitingSwift = true;
            }
            else if (gameObject.transform.localPosition.y <= initialY && direction < 0)
            {
                direction = 1;
                // if it was resetting to initial y then desactivate completely when done
                if (reseting)
                {
                    reseting = false;
                    if (currentActivations == 0)
                    {
                        active = false;
                    }
                }
                else
                {
                    waitingSwift = true;
                }
            }
            //gameObject.transform.Translate(new Vector3(0, direction * speed * Time.deltaTime, 0));
            gameObject.transform.position += new Vector3(0, direction * speed * Time.deltaTime, 0);
        }

    }


    public void OnCollisionEnter(Collision collision)
    {
       // collision.gameObject.transform.parent = gameObject.transform;
    }

    public void OnCollisionExit(Collision collision)
    {
        /*if (!collision.gameObject.CompareTag("Grabbable"))
        {
            collision.gameObject.transform.parent = null;
        }*/
    }

    public override void Activate(GameObject trigger)
    {
        currentActivations++;
        if (currentActivations >= activations)
        {
            active = true;
        }
    }

    public override void Desactivate()
    {
        reseting = true;
        direction = -1;
        currentActivations--;
    }
}
