using UnityEngine;
using System.Collections;

public class MovePatternSwitch : MonoBehaviour, Health {

    public MovePattern movePattern;
    public GameObject destination;

    public void TakeDamage(GameObject origin, int damage)
    {
        movePattern.includeOriginalPosition = true;
        movePattern.ClearPathPoints();
        movePattern.AddPathPoint(destination);
    }
}
