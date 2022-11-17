using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public int hp = 999;
    public int damage = 1;
    public int experience_reward = 400;
    public float Movespeed;
   

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.experience_reward = stats.experience_reward;
        this.Movespeed = stats.Movespeed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);        
    }
}
public class Enemy : MonoBehaviour, IDamageble
{
    Transform targetDestination;
    GameObject targetGameobject;
    Character targetCharacter;
    

    Level playerLevel;
    CarOrHumanManager carOrHumanManager; // my add
    GameObject carGameObject;//my add
    EnemiesAndKilled enemiesAndKilled; //my add
    
    Rigidbody2D rgdb2d;
    public SpriteRenderer spriteRenderer; // my add

    public EnemyStats stats;

   

    private void Awake()
    {
        rgdb2d = GetComponent<Rigidbody2D>();
        


    }

    void Start()
    {
       spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // my add
       // targetCharacter = targetGameobject.gameObject.GetComponent<Character>();
        carOrHumanManager = FindObjectOfType<CarOrHumanManager>(); // my add
        playerLevel = FindObjectOfType<Level>(); // my add
        enemiesAndKilled = FindObjectOfType<EnemiesAndKilled>(); // my add

    }

    void Update()
    {
        targetCharacter = targetGameobject.gameObject.GetComponent<Character>(); // added to update from start, should have when bool changes for if car or human
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
        rgdb2d.velocity = directin * stats.Movespeed;

        if((targetDestination.position.x - transform.position.x) < 0) //my add to have enemy look at player correct
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        } // my add
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
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

        targetCharacter.TakeDamage(stats.damage);
    }

    public void TakeDamage(int damage)
    {
        stats.hp -= damage;

        if(stats.hp < 1)
        {
            //targetGameobject.GetComponent<Level>().AddExperience(experience_reward);
            playerLevel.AddExperience(stats.experience_reward); //my change I had to make Level Script on Game Manager to get experience to add or enemy would not die
            GetComponent<DropOnDestroy>().CheckDrop();
            enemiesAndKilled.enemiesKilledNumber++; // my add
            Destroy(gameObject);
        }
    }
}
