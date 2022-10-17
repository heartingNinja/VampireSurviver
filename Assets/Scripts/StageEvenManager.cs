using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEvenManager : MonoBehaviour
{
    [SerializeField] StageData stageData;

    StageTimer stageTime;
    [SerializeField] EnemiesManger enemiesManger;
    int eventIndexer;

    private void Awake()
    {
        stageTime = GetComponent<StageTimer>();
    }

    private void Update()
    {

        if(eventIndexer >= stageData.stageEvenets.Count)
        {
            return;
        }
        if(stageTime.time > stageData.stageEvenets[eventIndexer].time)
        {
            Debug.Log(stageData.stageEvenets[eventIndexer].message);

            for(int i = 0; i < stageData.stageEvenets[eventIndexer].count; i++)
            {
                enemiesManger.SpawnEnemy();
            }

           
            eventIndexer += 1;
        }
    }
}
