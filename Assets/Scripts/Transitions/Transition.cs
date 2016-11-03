using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Transition : MonoBehaviour {

    public float fadeTime = 2;
    public float waitingTime = 2;
    public Color color = Color.white;
    public List<BaseTransitionHandler> transitionHandlers;

    private float currentTime;
    private int direction = 1;
    private bool begun;
    private bool ended = true;
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
    }

    public void StartTransition()
    {
        ended = false;
        begun = false;
        waiting = false;
        currentTime = 0;
        direction = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (ended)
        {
            return;
        }
        if (!begun)
        {
            begun = true;
            OnTransitionBegin();
        }
        if (waiting)
        {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime >= waitingTime)
            {
                waiting = false;
                currentTime = fadeTime;
                direction = -1;
                OnWaitingEnd();
            }
        }
        else
        {
            currentTime = currentTime + Time.deltaTime * direction;
            if (currentTime >= fadeTime)
            {
                waiting = true;
                currentTime = 0;
                OnWaitingBegin();
            }
            if (currentTime < 0)
            {
                ended = true;
                OnTransitionEnd();
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

    void OnTransitionBegin()
    {
        foreach(BaseTransitionHandler handler in transitionHandlers)
        {
            handler.OnTransitionBegin(this);
        }
    }

    void OnWaitingBegin()
    {

        foreach (BaseTransitionHandler handler in transitionHandlers)
        {
            handler.OnWaitingBegin(this);
        }
    }

    void OnWaitingEnd()
    {

        foreach (BaseTransitionHandler handler in transitionHandlers)
        {
            handler.OnWaitingEnd(this);
        }
    }

    void OnTransitionEnd()
    {

        foreach (BaseTransitionHandler handler in transitionHandlers)
        {
            handler.OnTransitionEnd(this);
        }
    }
}
