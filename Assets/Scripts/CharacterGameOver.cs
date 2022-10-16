using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] GameObject weaponPartent;
   public void GameOver()
    {
        Debug.Log("Game Over");
        GetComponent<PlayerMove>().enabled = false;
        gameOverPanel.SetActive(true);
        weaponPartent.SetActive(false);
    }
}
