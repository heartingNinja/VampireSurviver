using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float time;
    TimerUI timerUI;

    private void Awake()
    {
      //  timerUI = FindObjectOfType<TimerUI>();
    }

    private void Start()
    {
        timerUI = FindObjectOfType<TimerUI>();
    }

    void Update()
    {
        time += Time.deltaTime;
        timerUI.UpdateTime(time);
    }
}
