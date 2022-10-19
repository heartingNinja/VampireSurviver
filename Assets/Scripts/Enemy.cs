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
    SpriteRenderer spriteRenderer; // my add

    [SerializeField] int hp = 999;
    [SerializeField] int damage = 1;
    [SerializeField] int experience_reward = 400;

    private void Awake()
    {
        rgdb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // my add


    }

    void Start()
    {
        targetCharacter = targetGameobject.gameObject.GetComponent<Character>();
    }

    void Update()
    {

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

        if((targetDestination.position.x - transform.position.x) < 0) //my add to have enemy look at player correct
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        } // my add
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
