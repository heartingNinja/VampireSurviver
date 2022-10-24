using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasButton : MonoBehaviour
{
    public float gasSpeed;
    public bool gasOn;

    public void GasOn()
    {
        gasOn = true;
        gasSpeed = 1;
        Debug.Log("Gas On");
    }

    public void GasOff()
    {
        gasOn = false;
        gasSpeed = 0;
        Debug.Log("Gas Off");
    }
}
