using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Texture2D crosshairActiveTexture;
    private GauntletController gauntlet;

    void Start()
    {
        gauntlet = GetComponent<GauntletController>();
    }

    void OnGUI()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = false;

        Texture2D activeTexture;
        if (gauntlet.getTargetAnalyzer().InSight())
        {
            activeTexture = crosshairActiveTexture;
        }
        else
        {
            activeTexture = crosshairTexture;
        }
        Rect position = new Rect((Screen.width - activeTexture.width) / 2, (Screen.height - activeTexture.height) / 2, activeTexture.width, activeTexture.height);
        GUI.DrawTexture(position, activeTexture);
    }
}