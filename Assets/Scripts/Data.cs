using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    [Header("base stats")]
    public double health;
    public double maxHealth;
    public double bulletBaseDamage;
    public float bulletSpeed;
    public double attSpd;
    public double towerRange;

    [Header("initial stats")]
    public double money;
    public double shootTimer;
    public double wave;

    [Header("waves stuff")]
    public double waveDuration;
    public double waveTimer;
    public double waveTime;
    public float spawnDistance;
    public int waveSpawnA1;
    public int waveSpawnA0;

    [Header("Upgrade bonuses")]
    public double dmgPerUpgrade;
    public double attSpdPerUpgrade;
    public double attRangePerUpgrade;
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
        upgrades.Add("Attack range", new Upgrade() { upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 10 });
        upgrades.Add("Health", new Upgrade() { upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0 }); //not yet implemented
        upgrades.Add("Health regeneration", new Upgrade() { upgradeBaseCost = 1, upgradeMultCost = 1.4, upgradeMaxLevel = 0 });
        //foreach(KeyValuePair<string, Upgrade> entry in upgrades) // WARNING: wypluwa dwa razy
        //{
        //    print(entry.Key);
        //}
        //print(upgrades.Count);
    }

    public int GetWaveEnemiesCount()
    {
        return waveSpawnA1 * (int)wave + waveSpawnA0;
    }
    public double GetWaveEnemiesHealth()
    {
        return 2 + wave;
    }
    public double GetWaveEnemiesDps()
    {
        return 0.9 + wave / 10;
    }
    public double GetWaveEnemiesSpeed()
    {
        return 4 + wave / 3;
    }
    public double GetWaveEnemiesMoney()
    {
        return wave;
    }
    public double GetBulletDamage()
    {
        return bulletBaseDamage + upgrades["Damage"].upgradeLevel * dmgPerUpgrade;
    }
    public double GetAttspd()
    {
        return attSpd + attSpdPerUpgrade * upgrades["Attack speed"].upgradeLevel;
    }
}
