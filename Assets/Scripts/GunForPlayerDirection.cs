using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunForPlayerDirection : MonoBehaviour
{
    PlayerMove playerMove;

    [SerializeField] GameObject leftGunObject;
    [SerializeField] GameObject rightGunObject;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        ChangeGunPosition();
    }

    public void ChangeGunPosition()
    {
        if (playerMove.lastHorizontalVector > 0)
        {
            rightGunObject.SetActive(true);
            leftGunObject.SetActive(false);
        }
        if (playerMove.lastHorizontalVector < 0)
        {
            rightGunObject.SetActive(false);
            leftGunObject.SetActive(true);
        }
    }
}
