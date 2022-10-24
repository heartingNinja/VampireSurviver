using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PCorMobileManger : MonoBehaviour
{
    public bool isMobile;
    [SerializeField] GameObject mobileUI;

    public void Update()
    {
        PCorMobile();
    }

    public void PCorMobile()
    {
        if(isMobile)
        {
            mobileUI.SetActive(true);
        }
        else
        {
            mobileUI.SetActive(false);
        }
    }
}
