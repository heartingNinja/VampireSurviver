using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0, 0);   
    [SerializeField] Vector2Int playerTitlePosition;
    Vector2Int onTileGridPlayerPositon;
    [SerializeField] float titleSize = 20f;
    GameObject[,] terrainTiles;

    

    [SerializeField] int terrianTileHorizontalCount;
    [SerializeField] int terrianTileVerticalCount;

    [SerializeField] int fieldOfVistionHeight = 3;
    [SerializeField] int fieldOfViestionWidth = 3;


    private void Awake()
    {
        terrainTiles = new GameObject[terrianTileHorizontalCount, terrianTileVerticalCount];
    }

    private void Start()
    {
        UpdateTilesOnScreen();
        playerTransform = GameManager.instance.playerTransform;
    }

    private void Update()
    {
        playerTitlePosition.x = (int)(playerTransform.position.x / titleSize);
        playerTitlePosition.y = (int)(playerTransform.position.y / titleSize);

        playerTitlePosition.x -= playerTransform.position.x < 0 ? 1 : 00;
        playerTitlePosition.y -= playerTransform.position.y < 0 ? 1 : 00;

        if (currentTilePosition != playerTitlePosition)
        {
            currentTilePosition = playerTitlePosition;

            onTileGridPlayerPositon.x = CalculatePositonOnAxis(onTileGridPlayerPositon.x, true);
            onTileGridPlayerPositon.y = CalculatePositonOnAxis(onTileGridPlayerPositon.y, true);
            UpdateTilesOnScreen();

        }
    }

    public void UpdateTilesOnScreen()
    {
        for (int pov_x = -(fieldOfViestionWidth/2); pov_x <= fieldOfViestionWidth/2; pov_x++)
        {
            for (int pov_y = -(fieldOfVistionHeight/2); pov_y <= fieldOfVistionHeight/2; pov_y++)
            {
                int tileToUpdate_x = CalculatePositonOnAxis(playerTitlePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositonOnAxis(playerTitlePosition.y + pov_y, false);

    
                GameObject title = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                Vector3 newPosition = CalculatTiltePosistion(playerTitlePosition.x + pov_x, playerTitlePosition.y + pov_y);
               
                if(newPosition != title.transform.position)
                {
                    title.transform.position = newPosition;
                    terrainTiles[tileToUpdate_x, tileToUpdate_y].GetComponent<TerrainTitle>().Spawn();
                }

               
            }
        }
    }

    private Vector3 CalculatTiltePosistion(int x, int y)
    {
        return new Vector3(x * titleSize, y * titleSize, 0f);
    }

    private int CalculatePositonOnAxis(float currentValue, bool horizontal)
    {
        if(horizontal)
        {
           if(currentValue >= 0)
            {
                currentValue = currentValue % terrianTileHorizontalCount;
            }
           else
            {
                currentValue += 1;
                currentValue = terrianTileHorizontalCount -1 + currentValue % terrianTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrianTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrianTileVerticalCount -1 + currentValue % terrianTileVerticalCount;
            }
        }

        return (int)currentValue;
    }

    

    public void Add(GameObject titleGameObject, Vector2Int titlePosition)
    {
        terrainTiles[titlePosition.x, titlePosition.y] = titleGameObject;
    }
}
