using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingChoose : MonoBehaviour
{
    BuildingManger buildingManger;

    private void Start()
    {
        buildingManger = FindObjectOfType<BuildingManger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character c = collision.GetComponent<Character>();
        if (c != null && buildingManger.timeSinceLastChecked > 5)
        {
            buildingManger.buildingUI.SetActive(true);
            Time.timeScale = 0;
            buildingManger.timeSinceLastChecked = 0;
            
        }
    }
}
