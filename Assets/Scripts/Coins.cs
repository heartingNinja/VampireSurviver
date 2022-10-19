using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Coins : MonoBehaviour
{
    [SerializeField] DataContainer data;
    [SerializeField] TextMeshProUGUI coinsCountText;

    public void Add(int count)
    {
        data.coins += count;
        coinsCountText.text = "Coins: " + data.coins.ToString();
    }
}
