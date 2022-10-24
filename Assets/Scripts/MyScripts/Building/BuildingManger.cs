using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManger : MonoBehaviour
{
    public GameObject buildingUI;
    [SerializeField] DataContainer dataContainer;
    public float timeSinceLastChecked;
    Character character;
   // GameObject[] enemies;


    // Update is called once per frame
    void Update()
    {
        timeSinceLastChecked += Time.deltaTime;
    }

    public void RentRoomBonusandCost()
    {
       // enemies = GameObject.FindGameObjectsWithTag("Enemy");
        character = FindObjectOfType<Character>();
        character.currentHp = character.maxHP;
        buildingUI.SetActive(false);
        dataContainer.coins -= 5;
       // Destroy(enemies[enemies.Length]);
        Time.timeScale = 1;

    }

    public void CloseBuilding()
    {
        buildingUI.SetActive(false);
        Time.timeScale = 1;
    }
}
