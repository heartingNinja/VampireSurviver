using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesAndKilled : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesText;
    [SerializeField] TextMeshProUGUI enemiesKilledText;

    float enemiesNumber;
    public float enemiesKilledNumber;

    void Update()
    {
        enemiesNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;

        enemiesText.text = "ENEMIES:" + enemiesNumber.ToString();
        enemiesKilledText.text = "TERMINATED:" + enemiesKilledNumber.ToString();

    }
}
