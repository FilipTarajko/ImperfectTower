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

    void Awake()
    {
        maxHealth = 10;
        wave = 1;
        waveTimer = 0;
        waveTime = 3;
        health = maxHealth;
        spawnDistance = 20;
        towerRange = 10;
        shootTimer = 0;
        shootTime = 0.3;
        money = 0;
    }
}
