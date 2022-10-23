using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CointPickUp : MonoBehaviour, IPickUpObject
{
    [SerializeField] int count;
    Coins coins;

    private void Start()
    {
        coins = FindObjectOfType<Coins>();
    }
    public void OnPickUp(Character character)
    {
        // character.coins.Add(count); 
        coins.Add(count); // my change
    }
}
