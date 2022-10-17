using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSpearWeapon : WeaponBase
{
   
    PlayerMove playerMove;

    [SerializeField] GameObject spearPrefab;
    [SerializeField] float spread = 0.5f;

  

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    public override void Attack()
    {
       
        for(int i = 0; i <weaponStats.numberOfAttacks; i++)
        {
            GameObject thrownSpear = Instantiate(spearPrefab);
            Vector3 newSpearPositon = transform.position;

            if (weaponStats.numberOfAttacks > 1)
            {
                newSpearPositon.y -= (spread * (weaponStats.numberOfAttacks - 1)) / 2; // calculate offset
                newSpearPositon.y += i * spread; // spreading the knives along the line
            }
           
           

            thrownSpear.transform.position = newSpearPositon;

            ThrowingSpearProjectile throwingSpearProjectile = thrownSpear.GetComponent<ThrowingSpearProjectile>();
            throwingSpearProjectile.SetDirection(playerMove.lastHorizontalVector, 0f);
        }
      

    }


}
