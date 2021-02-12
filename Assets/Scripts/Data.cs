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

    void Awake()
    {
        maxHealth = 10;
        wave = 1;
        waveTimer = 1;
        waveTime = 1;
        health = maxHealth;
        spawnDistance = 20;
    }
}
