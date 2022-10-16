using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManger pauseManger;

    private void Awake()
    {
        pauseManger = GetComponent<PauseManger>();
    }

    public void OpenPanel()
    {
        pauseManger.PauseGame();
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        pauseManger.UnPauseGame();
        panel.SetActive(false);
    }
}
