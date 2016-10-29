using UnityEngine;
using System.Collections;

public class TeleportWaypoint : MonoBehaviour {

    public GameObject waypointObject;
    public AlignmentX alignmentX = AlignmentX.Center;
    public AlignmentY alignmentY = AlignmentY.Middle;
    public AlignmentZ alignmentZ = AlignmentZ.Center;
	
	public void TeleportObject (GameObject gameObject) {
        Vector3 pos = waypointObject.transform.position;
        Bounds bounds = gameObject.GetComponent<Collider>().bounds;

        float sizeX = bounds.size.x / 2;
        float sizeY = bounds.size.y / 2;
        float sizeZ = bounds.size.x / 2;

        pos.x += sizeX * (int)alignmentX;
        pos.y += sizeY * (int)alignmentY;
        pos.z += sizeZ * (int)alignmentZ;

        gameObject.transform.position = pos;
    }
}

public enum AlignmentX
{
    Left = 1, Center = 0, Right = -1
}

public enum AlignmentY
{
    Top = 1, Middle = 0, Bottom = -1
}

public enum AlignmentZ
{
    Front = 1, Center = 0, Back = -1
}