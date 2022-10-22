using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWeaponScript : WeaponBase
{
    [SerializeField] GameObject carGameObject;   


    [SerializeField] Vector2 attackSize = new Vector2(1f, 1f);

    CarInputHandler carInput;


    private void Awake()
    {
        carInput = GetComponentInParent<CarInputHandler>();
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
           
                Collider2D[] colliders = Physics2D.OverlapBoxAll(carGameObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);

            
           
            yield return new WaitForSeconds(.3f);
        }

    }
}

