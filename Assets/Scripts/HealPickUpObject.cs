using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int healAmount;
    CarOrHumanManager carOrHumanManager; // my add

    private void Start()
    {
        carOrHumanManager = FindObjectOfType<CarOrHumanManager>();
    }
    public void OnPickUp(Character character)
    {
        if(carOrHumanManager.isHuman)
        {
            character.Heal(healAmount);
        }
        
    }
}
