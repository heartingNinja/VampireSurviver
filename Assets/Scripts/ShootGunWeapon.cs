using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootGunWeapon : WeaponBase
{
    [SerializeField] GameObject leftShotObject;
    [SerializeField] GameObject rightShotObject;
    

    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);

    PlayerMove playerMove;


    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }


    private void ApplyDamage(Collider2D[] colliders)
    {
      
        foreach (Collider2D enemyCollider in colliders)
        {
            IDamageble enemy = enemyCollider.GetComponent<IDamageble>();
            if (enemy != null)
            {
                PostDamage(weaponStats.damage, enemyCollider.transform.position);
                enemy.TakeDamage(weaponStats.damage);
            }
        }
    }

    public override void Attack()
    {
        StartCoroutine(AttackProcess());


    }

    

    

    IEnumerator AttackProcess()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            if (playerMove.lastHorizontalVector > 0)
            {
                rightShotObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightShotObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);

            }
            else
            {

                leftShotObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftShotObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }

            yield return new WaitForSeconds(.3f);
        }

    }
}
