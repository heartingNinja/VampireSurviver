using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<Item> items;

    Character character;

    // my add
    Item itemToUpgrade;

    private void Awake()
    {
       // character = GetComponent<Character>();
    }

    private void Start()
    {
        character = FindObjectOfType<Character>();
    }

    private void Update()
    {
        // my add, should only look for character when changes to car
       // character = FindObjectOfType<Character>();
    }

    public void Equip(Item itemToEquip)
    {
        if(items == null)
        {
            items = new List<Item>();
        }
        Item newItemInstance = new Item();
        newItemInstance.Init(itemToEquip.Name);
        newItemInstance.stats.Sum(itemToEquip.stats);


        items.Add(newItemInstance);
        newItemInstance.Equip(character);

        //my add
        itemToUpgrade = newItemInstance;
    }

    public void UnEquip(Item itemToUnequip)
    {

    }

    internal void UpgradeItem(UpgradeData upgradeData)
    {
        // changed as can not get items.Find to work
        // Item itemToUpgrade = items.Find(id => id.Name == upgradeData.Name);

        

        itemToUpgrade.UnEquip(character);
        itemToUpgrade.stats.Sum(upgradeData.itemStats);
        itemToUpgrade.Equip(character);
    }
}
