using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePattern : MonoBehaviour
{
    public enum LoopMode
    {
        NONE, RING, REVERSE, RESTART
    }
    /**
     * Only used when REVERSE for now!
     * */
    public float swiftDelay = 0;
    public List<GameObject> positionObjects;
    public bool includeOriginalPosition;
    public LoopMode loopMode = LoopMode.NONE;
    public float timePerPosition = 10;
    // directions that will affect
    public bool affectX = true;
    public bool affectY = true;
    public bool affectZ = true;
    public bool stopOnCollision;
    private float currentTime = 0;
    private Vector3 lastObjectPosition;
    private int currentIndex = 0;
    private int loopCount;
    private int indexTraversalDirection = 1;
    private Rigidbody rigidBody;
    private List<GameObject> currentCollisions = new List<GameObject>();
    private bool waitingSwift;
    private float swiftCounter;
    private Vector3 temp;
    private Vector3 centerToBoundLimit = Vector3.zero;
    /**
     * Contains the original position of this 
     * entity before the loop cycle begins 
     * */
    private GameObject originObject;
    private bool dirty = true;

    void Awake()
    {
        originObject = new GameObject();
        originObject.name = name + " original position";
        originObject.transform.parent = gameObject.transform.parent;
        originObject.transform.position = transform.position;
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Initialize();
    }

    private void Initialize()
    {
        currentIndex = 0;
        currentTime = 0;
        loopCount = 0;
        swiftCounter = 0;
        waitingSwift = false;
        indexTraversalDirection = 1;
        if (dirty)
        {
            originObject.transform.position = transform.position;
            /* if true this will insert the original position 
             * of this object as the first point in the path */
            if (includeOriginalPosition)
            {
                positionObjects.Insert(0, originObject);
            }
        }
        /* Set the current position to the position 
         * of the first element in the list */
        if (positionObjects.Count > 0)
        {
            transform.position = positionObjects[0].transform.position;
            currentIndex = 1;
        }
        lastObjectPosition = transform.position;
        dirty = false;
    }

    void FixedUpdate()
    {
        if (dirty)
        {
            Reset();
        }
        if (IsIndexOutOfBounds())
        {
            return;
        }
        if (waitingSwift)
        {
            swiftCounter += Time.fixedDeltaTime;
            if (swiftCounter >= swiftDelay)
            {
                waitingSwift = false;
                swiftCounter = 0;
            }
            return;
        }
        MoveToNextPosition();
    }

    private void MoveToNextPosition()
    {
        float timeStep = currentTime + Time.fixedDeltaTime;
        float step = timeStep / timePerPosition;
        GameObject nextPositionObject = positionObjects[currentIndex];
        // Get new position between old point and new point
        Vector3 newPosition = GetNewPosition(nextPositionObject.transform.position, step);
        bool result = SetPosition(newPosition);
        if (!result)
        {
            ClearPathPoints();
            return;

        }
        currentTime = timeStep;
        // Change target if we have reached our current target
        if (EqualPositions(newPosition, nextPositionObject.transform.position))
        {
            currentTime = 0;
            lastObjectPosition = newPosition;
            HandleTargetChange();
        }
    }

    private bool resolveCollisions(Vector3 newPosition, out Vector3 resolvedPosition)
    {
        Vector3 currentPosition = transform.position;
        var heading = newPosition - currentPosition;
        var distanceToMove = heading.magnitude;
        var direction = heading / distanceToMove;
        Bounds bounds = gameObject.GetComponentInChildren<Collider>().bounds;
        centerToBoundLimit.x = bounds.size.x * direction.x / 2;
        centerToBoundLimit.y = bounds.size.y * direction.y / 2;
        centerToBoundLimit.z = bounds.size.z * direction.z / 2;
        float distanceFromCenterToBounds = centerToBoundLimit.magnitude;
        float raycastDistance = distanceToMove + distanceFromCenterToBounds;
        RaycastHit hitInfo;
        if (Physics.Raycast(bounds.center, direction, out hitInfo, raycastDistance))
        {
            if (!hitInfo.transform.IsChildOf(transform) && !hitInfo.collider.isTrigger)
            {
                resolvedPosition = currentPosition;
                float correctedDistanceToMove = hitInfo.distance - distanceFromCenterToBounds;
                resolvedPosition.x += direction.x * correctedDistanceToMove;
                resolvedPosition.y += direction.y * correctedDistanceToMove;
                resolvedPosition.z += direction.z * correctedDistanceToMove;
                return true;
            }
        }
        resolvedPosition = newPosition;
        return false;
    }

    private bool SetPosition(Vector3 position)
    {
        // Apply only enabled axis to the new position
        Vector3 newPosition = ResolveConditionalPosition(transform.position, position);
        bool collided = false;
        if (stopOnCollision)
        {
            Vector3 resolvedPosition;
            collided = resolveCollisions(newPosition, out resolvedPosition);
            newPosition = resolvedPosition;
        }
        if (rigidBody != null)
        {
            rigidBody.MovePosition(newPosition);
        }
        else
        {
            transform.position = newPosition;
        }
        if (stopOnCollision && collided)
        {
            return false;
        }
        return true;
    }

    private bool IsIndexOutOfBounds()
    {
        return currentIndex > positionObjects.Count - 1 || currentIndex < 0;
    }

    private void HandleTargetChange()
    {
        currentIndex = currentIndex + 1 * indexTraversalDirection;
        if (IsIndexOutOfBounds())
        {
            HandleLoopEnd();
        }
    }

    /**
     * Handles the actions to perform when a loop cycle ends.
     */
    private void HandleLoopEnd()
    {
        loopCount++;
        switch (loopMode)
        {
            case LoopMode.RING:
                currentIndex = 0;
                break;
            case LoopMode.REVERSE:
                indexTraversalDirection *= -1;
                currentIndex = currentIndex + 2 * indexTraversalDirection;
                waitingSwift = true;
                break;
            case LoopMode.RESTART:
                currentIndex = 1;
                Vector3 newPosition = positionObjects[0].transform.position;
                SetPosition(newPosition);
                lastObjectPosition = newPosition;
                OnLoopRestart();
                break;
        }
    }

    /**
     * Handles the actions to perform when a loop is restarted.
     */
    private void OnLoopRestart()
    {
        // hardcoding this part because of time
        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }
    }

    /**
     * Returns the position to apply between the last object position 
     * and the next object position, depending on the given step (between 0 and 1).
     */
    private Vector3 GetNewPosition(Vector3 nextObjectPosition, float step)
    {
        return Vector3.Lerp(lastObjectPosition, nextObjectPosition, step);
    }

    /**
     * Compares two positions and returns true if they are equal. 
     * Only the axis active in the pattern movement are used in the comparison.
     */
    private bool EqualPositions(Vector3 position1, Vector3 position2)
    {
        bool equalX = affectX ? position1.x == position2.x : true;
        bool equalY = affectY ? position1.y == position2.y : true;
        bool equalZ = affectZ ? position1.z == position2.z : true;
        return equalX && equalY && equalZ;
    }

    /**
     * Returns a position set to the newPosition only for the axis that are 
     * enabled in the pattern movement, for the ones that are not enabled 
     * they are set to the current position.
     */
    private Vector3 ResolveConditionalPosition(Vector3 currentPosition, Vector3 newPosition)
    {
        temp.x = affectX ? newPosition.x : currentPosition.x;
        temp.y = affectY ? newPosition.y : currentPosition.y;
        temp.z = affectZ ? newPosition.z : currentPosition.z;
        return temp;
    }

    /**
     * Restarts this component and starts the loops from the begining.
     */
    public void Reset()
    {
        Initialize();
        OnLoopRestart();
    }

    /**
     * Removes all the points of the path list.
     */
    public void ClearPathPoints()
    {
        positionObjects.Clear();
        dirty = true;
    }

    /**
     * Adds a GameObject to the end of the list of path points.
     */
    public void AddPathPoint(GameObject gameObject)
    {
        positionObjects.Add(gameObject);
    }
}