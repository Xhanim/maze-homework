using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface ITargetAnalyzer
{
    /**
     * Return true if you want the crosshair to be active.
     * */
    bool InSight();

    Texture2D GetInSightTexture();
}
