using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : WeaponBase
{  
    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;
    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);

    PlayerMove playerMove;
   

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

   
    private void ApplyDamage(Collider2D[] colliders)
    {

        //OG System
      //  for(int i = 0; i < colliders.Length; i++)
      //  {
      //      Enemy e = colliders[i].GetComponent<Enemy>();
      //      if(e != null)
      //      {
      //          colliders[i].GetComponent<Enemy>().TakeDamage(whipDamage);
      //      }
           
     //   }
       

        //Mine
        foreach(Collider2D enemyCollider in colliders)
        {
            IDamageble enemy = enemyCollider.GetComponent<IDamageble>();
            if(enemy != null)
            {
                PostDamage(weaponStats.damage, enemyCollider.transform.position);
                enemy.TakeDamage(weaponStats.damage);
            }
        }
    }

    public override void Attack()
    {
       
        if (playerMove.lastHorizontalVector > 0)
        {
            rightWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, attackSize, 0f);
            ApplyDamage(colliders);

        }
        else
        {

            leftWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, attackSize, 0f);
            ApplyDamage(colliders);
        }
    }
}
