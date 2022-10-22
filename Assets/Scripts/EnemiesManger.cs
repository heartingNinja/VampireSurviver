using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManger : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float SpawnTimer;
    GameObject player;
    GameObject car; // my add
    public GameObject[] enemies;
    
    CarOrHumanManager carOrHumanManager; // my ADD
    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
        carOrHumanManager = FindObjectOfType<CarOrHumanManager>();
    }

    private void Update() // My Add
    {

        // This should all be part of changigng to car and back to human
       enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        
       
       if(carOrHumanManager.isHuman == false)
        {
            car = FindObjectOfType<TopDownCarController>().gameObject;

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().SetTarget(car);
            }
        }
       else
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().SetTarget(player);
            }
        }

      
    } // all update my add


    public void SpawnEnemy()
    {
        Vector3 position = GenerateRandomPosition();

        if(carOrHumanManager.isHuman == false) // my add
        {
            position += car.transform.position;
        }
        else
        {
            position += player.transform.position;
        } // end my add

       // position += player.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().SetTarget(player);
        newEnemy.transform.parent = transform;
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 positon = new Vector3();
        float f = Random.value > .5f ? -1f : 1f;

        if(Random.value > .5f)
        {
            positon.x = Random.Range(-spawnArea.x, spawnArea.x);
            positon.y = spawnArea.y * f;
        }
        else
        {
            positon.y = Random.Range(-spawnArea.y, spawnArea.y);
            positon.x = spawnArea.x * f;
        }
        
       
        positon.z = 0;
         

        return positon;
    }
}
