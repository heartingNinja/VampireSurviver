using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int healAmount;
    CarOrHumanManager carOrHumanManager; // my add

    void Start()
    {
        carOrHumanManager = FindObjectOfType<CarOrHumanManager>();
    }

    public void OnPickUp(Character character)
    {
        if (!carOrHumanManager.isHuman)
        {
            character.Heal(healAmount);
        }
    }


}
