using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NukeDamage : WeaponBase
{
    [SerializeField] GameObject nuke;

    [SerializeField] Vector2 damageSize = new Vector2(10f, 10f);
    [SerializeField] WeaponBase weaponBase;

  
   
    

    private void NukeDamageEnemy(Collider2D[] colliders)
    {
        foreach (Collider2D enemyCollider in colliders)
        {
            IDamageble enemy = enemyCollider.GetComponent<IDamageble>();
            if (enemy != null)
            {
                weaponBase.PostDamage(weaponStats.damage, enemyCollider.transform.position);
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
        
                Collider2D[] colliders = Physics2D.OverlapBoxAll(nuke.transform.position, damageSize, 0f);
                NukeDamageEnemy(colliders);

            
            

            yield return new WaitForSeconds(.3f);
        

    }
}
