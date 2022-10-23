using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManger pauseManger;

    [SerializeField] List<UpgradeButton> upgradeButtons;
    private void Awake()
    {
        pauseManger = GetComponent<PauseManger>();
    }

    private void Start()
    {
        HideButton();
    }
    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();

        pauseManger.PauseGame();
        panel.SetActive(true);

       
        for(int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void Clean()
    {
        for(int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }

    public void Upgrade(int pressedButtonID)
    {
       // GameManager.instance.playerTransform.GetComponent<Level>().Upgrades(pressedButtonID); // orginal
        GameManager.instance.GetComponent<Level>().Upgrades(pressedButtonID); // my change
        ClosePanel();
    }

    public void ClosePanel()
    {
        HideButton();

        pauseManger.UnPauseGame();
        panel.SetActive(false);
    }

    private void HideButton()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}
