using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProgress : MonoBehaviour
{
    StageTimer stageTimer;

    private void Awake()
    {
        stageTimer = GetComponent<StageTimer>();
    }

    [SerializeField] float progressTimeRate = 30f;
    [SerializeField] float progressPerSplit = .3f;

    public float Progress
    {
        get
        {
            return 1f + stageTimer.time / progressTimeRate * progressPerSplit;
        }
    }
}
