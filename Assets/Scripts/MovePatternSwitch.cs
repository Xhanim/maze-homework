using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePatternSwitch : MonoBehaviour, Health {

    public MovePattern movePattern;
    public List<GameObject> destinations;

    public void TakeDamage(GameObject origin, int damage)
    {
        movePattern.includeOriginalPosition = true;
        movePattern.ClearPathPoints();
        foreach(GameObject gameObject in destinations)
        {
            movePattern.AddPathPoint(gameObject);
        }
    }
}
