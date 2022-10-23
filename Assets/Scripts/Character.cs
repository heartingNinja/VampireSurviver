using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHP = 1000;
    public int currentHp = 1000;

    public int armor = 0;

    public float hpRegenerationRate = 1f;
    public float hpRegenerationTimer;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private bool isDead;

    [SerializeField] StatusBar hpBar;
    CarOrHumanManager carOrHumanManager; //my add
    
   

    

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start()
    {      
        carOrHumanManager = FindObjectOfType<CarOrHumanManager>();// my add
        hpBar.SetState(currentHp, maxHP);
        
    }

    private void Update()
    {
        DeathOrCarDestroy();

        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;

        if(hpRegenerationTimer > 1f)
        {
            Heal(1);
            hpRegenerationTimer -= 1f;
        }
    }
    public void TakeDamage(int damage)
    {
        if(isDead == true)
        {
            return;
        }
        ApplyArmor(ref damage);

        currentHp -= damage;

      //  if(currentHp <= 0 ) 
      //  {
      //      GetComponent<CharacterGameOver>().GameOver();
      //      isDead = true;
      //  }
       
        hpBar.SetState(currentHp, maxHP);
    }

    private void ApplyArmor(ref int damage)
    {
        damage -= armor;
        if(damage < 0)
        {
            damage = 0;
        }
    }

    public void Heal(int amount)
    {
        if (currentHp <= 0)
        {
            return;
        }

        currentHp += amount;

        if(currentHp > maxHP)
         {
           currentHp = maxHP;
         }

        hpBar.SetState(currentHp, maxHP);
    }

   public void DeathOrCarDestroy() //my test for death or car destroy
    {
        if (currentHp <= 0 && carOrHumanManager.isHuman) // && my add
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
        }

        if (currentHp <= 0 && carOrHumanManager.isHuman == false) // my add if car is at 0 health
        {
            carOrHumanManager.isHuman = true;
            
        }
    }
}
