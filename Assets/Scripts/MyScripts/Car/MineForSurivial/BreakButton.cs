using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakButton : MonoBehaviour
{
    public float breakSpeed;

    public void BreakOn()
    {
        breakSpeed = -1;
    }

    public void BreakOff()
    {
        breakSpeed = 0;
    }
}
