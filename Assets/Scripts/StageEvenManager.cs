using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEvenManager : MonoBehaviour
{
    [SerializeField] StageData stageData;

    StageTimer stageTime;
    [SerializeField] EnemiesManger enemiesManger;
    int eventIndexer;

    int rounds = 1; // my add

    private void Awake()
    {
        stageTime = GetComponent<StageTimer>();
    }

    private void Update()
    {
         // my add

        if(eventIndexer >= stageData.stageEvenets.Count)
        {
            rounds++; // my add
          // if(rounds >= 4) // my add
          //  {
           //     rounds = 4;
          //  }
            stageTime.time = -10; // my add 
            eventIndexer = 0; // my add
            return;
        }
        if(stageTime.time > stageData.stageEvenets[eventIndexer].time)
        {
           // stageData.stageEvenets[eventIndexer].count = stageData.stageEvenets[eventIndexer].count * rounds; // my add

            Debug.Log(stageData.stageEvenets[eventIndexer].message);

            for(int i = 0; i < stageData.stageEvenets[eventIndexer].count * rounds; i++) // added * rounds
            {
                enemiesManger.SpawnEnemy();
            }

             
             eventIndexer += 1;

           
        }

       
    }
}
