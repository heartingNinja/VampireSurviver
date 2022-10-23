using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int amount;
    Level level; // my add

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }
    public void OnPickUp(Character character)
    {
       // character.level.AddExperience(amount);
        level.AddExperience(amount);
    }
}
