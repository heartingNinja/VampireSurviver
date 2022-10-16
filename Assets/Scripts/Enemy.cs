using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    Transform targetDestination;
    GameObject targetGameobject;
    Character targetCharacter;
    [SerializeField] float speed;


    Rigidbody2D rgdb2d;

    [SerializeField] int hp = 999;
    [SerializeField] int damage = 1;
    [SerializeField] int experience_reward = 400;

    private void Awake()
    {
        rgdb2d = GetComponent<Rigidbody2D>();
       

    }

    void Start()
    {
        targetCharacter = targetGameobject.gameObject.GetComponent<Character>();
    }

    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;

        //my code
      //  targetCharacter = targetDestination.gameObject.GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        Vector3 directin = (targetDestination.position - transform.position).normalized;
        rgdb2d.velocity = directin * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       
        if (collision.gameObject == targetGameobject)
        {
            Attack();
        }
    }

   
    void Attack()
    {
       // OG
      //  if(targetCharacter == null)
     //   {
     //       targetCharacter = targetGameobject.GetComponent<Character>();
     //   }

        targetCharacter.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp < 1)
        {
            targetGameobject.GetComponent<Level>().AddExperience(experience_reward);
            GetComponent<DropOnDestroy>().CheckDrop();
            Destroy(gameObject);
        }
    }
}
