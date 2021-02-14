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
        upgrades.Add(new Upgrade() { upgradeName = "Damage", upgradeBaseCost = 10, upgradeMultCost = 1.4, upgradeMaxLevel = 0 });
        print(upgrades);
    }
}
