using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarDamage : WeaponBase
{
    [SerializeField] GameObject carGameObjectTop;
    [SerializeField] GameObject carGameObjectBottom;

    [SerializeField] Vector2 damageSize = new Vector2(2f, 4f); // the vector needs to change on player direction change
    [SerializeField] WeaponBase weaponBase;
    //[SerializeField] BoxCollider2D carCollider;

    [SerializeField] Rigidbody2D carRigidbody2D;

    TopDownCarController topDownCarController;
    private void Start()
    {
        topDownCarController = GetComponent<TopDownCarController>();
    }




    private void HitDamageCar(Collider2D[] colliders)
    {
        int speed = (int) carRigidbody2D.velocity.magnitude;
        foreach (Collider2D enemyCollider in colliders)
        {
            IDamageble enemy = enemyCollider.GetComponent<IDamageble>();
            if (enemy != null)
            {
                weaponBase.PostDamage(weaponStats.damage * speed, enemyCollider.transform.position);
                enemy.TakeDamage(weaponStats.damage * speed);
            }
        }
    }

    public override void Attack()
    {
        StartCoroutine(AttackProcess());


    }
    IEnumerator AttackProcess()
    {
       if(topDownCarController.accelerationInput > 0)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(carGameObjectTop.transform.position, damageSize, 0f);

            HitDamageCar(colliders);
        }
       else
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(carGameObjectBottom.transform.position, damageSize, 0f);

            HitDamageCar(colliders);
        }
        




        yield return new WaitForSeconds(0f);


    }
}
