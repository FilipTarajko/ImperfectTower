using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public double health;
    public double maxHealth;
    public double wave;
    public double waveTimer;
    public double waveTime;
    public float spawnDistance;
    public double towerRange;
    public double shootTimer;
    public double shootTime;
    public double money;
    public float bulletSpeed;
    public double bulletBaseDamage;
    public double dmgPerUpgrade;
    public double attspdPerUpgradeMult;
    public double healthPerUpgrade;
    public double hpRegenPerUpgrade;

    public class Upgrade
    {
        public string upgradeName;
        public double upgradeBaseCost;
        public double upgradeMultCost;
        public int upgradeMaxLevel;
        public int upgradeLevel = 0;
    }

    public List<Upgrade> upgrades = new List<Upgrade>() { };

    void Awake()
    {
        upgrades.Add(new Upgrade() { upgradeName = "Damage", upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0 });
        upgrades.Add(new Upgrade() { upgradeName = "Attack Speed", upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0 });
        upgrades.Add(new Upgrade() { upgradeName = "Health", upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0 }); //not yet implemented
        upgrades.Add(new Upgrade() { upgradeName = "Health regeneration", upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0 });
        foreach(Upgrade upgrade in upgrades) // wypluwa dwa razy
        {
            print(upgrade.upgradeName);
        }
        print(upgrades.Count);
    }
}
