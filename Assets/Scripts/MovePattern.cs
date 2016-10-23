using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePattern : MonoBehaviour
{

    public enum LoopMode
    {
        NONE, LOOP, REVERSE
    }

    public List<GameObject> positionObjects;
    public LoopMode loopMode = LoopMode.NONE;
    public float timePerPosition = 10;
    private float currentTime = 0;
    private Vector3 lastPosition;
    private int currentIndex = 0;
    private int loopCount;
    private int direction = 1;
    private Rigidbody rigidBody;
    private List<GameObject> currentCollisions = new List<GameObject>();

    void Start()
    {
        lastPosition = transform.position;
        if (loopMode != LoopMode.NONE)
        {
            GameObject originObject = new GameObject();
            originObject.transform.position = lastPosition;
            positionObjects.Add(originObject);
        }
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (positionObjects.Count == 0 || loopCount > 0 && loopMode == LoopMode.NONE || loopCount > 0 && positionObjects.Count == 1)
        {
            return;
        }
        GameObject currentObject = positionObjects[currentIndex];
        currentTime += Time.fixedDeltaTime;
        float step = currentTime / timePerPosition;
        Vector3 newPosition = getNewPosition(currentObject.transform.position, step);
        if (newPosition == currentObject.transform.position)
        {
            loopCount++;
            currentTime = 0;
            lastPosition = newPosition;
            updateIndex();
        }
        Vector3 currentPosition = transform.position;
        if (rigidBody != null)
        {
            rigidBody.MovePosition(newPosition);
        }
        else
        {
            transform.position = newPosition;
        }
    }

    private void updateIndex()
    {
        currentIndex = currentIndex + 1 * direction;
        if (currentIndex > positionObjects.Count - 1 || currentIndex < 0)
        {
            if (loopMode == LoopMode.LOOP)
            {
                currentIndex = 0;
            }
            else if (loopMode == LoopMode.REVERSE)
            {
                direction *= -1;
                currentIndex = currentIndex + 2 * direction;
            }
        }
    }

    private Vector3 getNewPosition(Vector3 nextPosition, float step)
    {
        Vector3 newPosition;
        if (step >= 1)
        {
            newPosition = nextPosition;
        }
        else
        {
            newPosition = Vector3.Lerp(lastPosition, nextPosition, step);
        }
        return newPosition;
    }
}