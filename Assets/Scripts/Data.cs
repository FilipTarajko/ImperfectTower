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
        public double upgradeBaseCost;
        public double upgradeMultCost;
        public int upgradeMaxLevel;
        public int upgradeLevel = 0;
    }

    public Dictionary<string, Upgrade> upgrades = new Dictionary<string, Upgrade>() { };

    void Awake()
    {
        upgrades.Add("Damage",  new Upgrade() { upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0 });
        upgrades.Add("Attack speed", new Upgrade() { upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0 });
        upgrades.Add("Health", new Upgrade() { upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0 }); //not yet implemented
        upgrades.Add("Health regeneration", new Upgrade() { upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0, upgradeLevel = 1 });
        foreach(KeyValuePair<string, Upgrade> entry in upgrades) // WARNING: wypluwa dwa razy
        {
            print(entry.Key);
        }
        print(upgrades.Count);
    }
}
