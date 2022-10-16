using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSpearWeapon : WeaponBase
{
   
    PlayerMove playerMove;

    [SerializeField] GameObject spearPrefab;

  

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    public override void Attack()
    {
        GameObject thrownSpear = Instantiate(spearPrefab);
        thrownSpear.transform.position = transform.position;
        thrownSpear.GetComponent<ThrowingSpearProjectile>().SetDirection(playerMove.lastHorizontalVector, 0f);
    }


}
