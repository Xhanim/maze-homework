using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
 * Used by the glove to determinate if a power has something of interest in sight and if it should apply a particular crosshair to it.
 * */
public interface ITargetAnalyzer
{
    /**
     * Return true if you want the crosshair to be active.
     * */
    bool InSight();

    Texture2D GetInSightTexture();
}
