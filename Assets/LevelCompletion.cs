using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] float timeToCompleteLevel;

    StageTimer stageTime;
    PauseManger pauseManger;

    [SerializeField] GameWinPanel levelCompletePanel;

    private void Awake()
    {
       // stageTime = GetComponent<StageTimer>();
       // pauseManger = FindObjectOfType<PauseManger>(true);
       // levelCompletePanel = FindObjectOfType<GameWinPanel>(true);
    }

    private void Start()
    {
        stageTime = GetComponent<StageTimer>();
        pauseManger = FindObjectOfType<PauseManger>();
        levelCompletePanel = FindObjectOfType<GameWinPanel>(true);
    }

    private void Update()
    {
        if(stageTime.time > timeToCompleteLevel)
        {
            pauseManger.PauseGame();
            levelCompletePanel.gameObject.SetActive(true);
        }
    }
}
