using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
 * Used by the switches, when a switch is activated it will call all the gameobjects liked to it that implement this interface.
 * */
public interface IActivator
{
    void Activate(GameObject trigger);
}
