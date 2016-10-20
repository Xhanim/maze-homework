﻿using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Texture2D crosshairActiveTexture;
    public Rect position;
    private GauntletController gauntlet;

    void Start()
    {
        gauntlet = GetComponent<GauntletController>();
        position = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height - crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
    }

    void OnGUI()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = false;
        
        if(gauntlet.getTargetAnalyzer().InSight())
        {
            GUI.DrawTexture(position, crosshairActiveTexture);
        }
        else
        {
            GUI.DrawTexture(position, crosshairTexture);
        }
    }
}