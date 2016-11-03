using UnityEngine;
using System.Collections;
using System;

public class BaseTransition : MonoBehaviour {

    public float fadeTime = 2;
    public float waitingTime = 2;
    public Color color = Color.white;
    private float currentTime;
    private int direction = 1;
    private bool begun;
    private bool ended;
    private bool waiting;
    private Texture2D texture;
    private GUIStyle guiStyle;


    void Start()
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.Apply();
        guiStyle = new GUIStyle();
        guiStyle.normal.background = texture;
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!begun)
        {
        }
        if (waiting)
        {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime >= waitingTime)
            {
            }
        } else if (!ended)
        {
            currentTime = currentTime + Time.deltaTime * direction;
            if (currentTime >= fadeTime)
            {
            }
            if (currentTime < 0)
            {
            }
        }
    }

    void OnGUI()
    {
        if (!ended)
        {
            Color lastColor = GUI.color;
            float alpha;
            if (waiting)
            {
                alpha = 1;
            } else
            {
                alpha = Mathf.Lerp(0, 1, currentTime / fadeTime);
            }
            Color newColor = color;
            newColor.a = alpha;
            GUI.color = newColor;
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), GUIContent.none, guiStyle);
            GUI.color = lastColor;
        }
    }

    public virtual void OnTransitionBegin()
    {

    }

    public virtual void OnWaitingBegin()
    {

    }

    public virtual void OnWaitingEnd()
    {

    }

    public virtual void OnTransitionEnd()
    {

    }
}
