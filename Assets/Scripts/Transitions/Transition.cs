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
    private bool started;
    private bool active;
    private bool waitingBeforeFadeout;
    private Texture2D texture;
    private GUIStyle guiStyle;
    private Rect screenRectangle;
    

    void Start()
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.Apply();
        guiStyle = new GUIStyle();
        guiStyle.normal.background = texture;
        screenRectangle = new Rect();
    }

    public void StartTransition()
    {
        active = true;
        started = false;
        waitingBeforeFadeout = false;
        currentTime = 0;
        direction = 1;
    }
	
	void Update () {
        if (!active)
        {
            return;
        }
        currentTime = currentTime + Time.deltaTime * direction;
        if (!started)
        {
            started = true;
            OnTransitionBegin();
        }
        if (waitingBeforeFadeout)
        {
            if (currentTime >= waitingTime)
            {
                handleWaitingEnd();
            }
        }
        else
        {
            /* 
             * If the current time bigger than the fadeTime it means 
             * the fadeIn has finished but if it's lesser than 0 it 
             * means the fadeOut has finished. 
             */
            if (currentTime >= fadeTime)
            {
                handleFadeInEnd();
            }
            else if (currentTime < 0)
            {
                handleFadeOutEnd();
            }
        }
    }

    void handleWaitingEnd()
    {
        waitingBeforeFadeout = false;
        currentTime = fadeTime;
        direction = -1;
        OnWaitingEnd();
    }

    void handleFadeInEnd()
    {
        waitingBeforeFadeout = true;
        currentTime = 0;
        OnWaitingBegin();
    }

    void handleFadeOutEnd()
    {
        active = false;
        OnTransitionEnd();
    }

    void OnGUI()
    {
        if (active)
        {
            DrawRectangle();
        }
    }

    void DrawRectangle()
    {
        Color lastColor = GUI.color;
        float alpha = 1;
        if (!waitingBeforeFadeout)
        {
            alpha = Mathf.Lerp(0, 1, currentTime / fadeTime);
        }
        Color newColor = color;
        newColor.a = alpha;
        GUI.color = newColor;
        screenRectangle.width = Screen.width;
        screenRectangle.height = Screen.height;
        GUI.Box(screenRectangle, GUIContent.none, guiStyle);
        GUI.color = lastColor;
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
