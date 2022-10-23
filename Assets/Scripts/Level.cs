using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //This was on player I moved to GameManger

    int level = 1;
    int experience = 0;
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanelManager;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;

    [SerializeField] List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;

    private void Awake()
    {
        // weaponManager = GetComponent<WeaponManager>(); 
        //my change
        weaponManager = FindObjectOfType<WeaponManager>();
    }
    private void Start()
    {
        experienceBar.UpdateExerienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);

        
    }

    internal void AddUpgradesIntoTheListOfAviableUpgrades(List<UpgradeData> upgradesAdd)
    {
        this.upgrades.AddRange(upgradesAdd);
    }

    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExerienceSlider(experience, TO_LEVEL_UP);
    }

    internal void Upgrades(int selectedUpgradeID)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];

        if(acquiredUpgrades == null)
        {
            acquiredUpgrades = new List<UpgradeData>();
        }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgradeData.weaponData);
                Debug.Log("Add Weapon");
                break;
            case UpgradeType.ItemUnlock:
                break;
        }

        acquiredUpgrades.Add(upgradeData);

        upgrades.Remove(upgradeData);
    }

    public void CheckLevelUp()
    {
        if(experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if(selectedUpgrades == null)
        {
            selectedUpgrades = new List<UpgradeData>();
        }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(4));

        upgradePanelManager.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if(count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for(int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }
        

        return upgradeList;
    }
}
