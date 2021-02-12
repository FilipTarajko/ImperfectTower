using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Data data;
    public TMP_Text healthDisplay;
    public TMP_Text waveDisplay;
    public BasicEnemy basicEnemy;
    public GameObject Tower;

    void Start()
    {
        data = Object.FindObjectOfType<Data>();
    }

    // Update is called once per frame
    void Update()
    {
        if (data.health <= 0)
        {
            SceneManager.LoadSceneAsync(0);
        }
        else
        {
            if (data.waveTimer >= data.waveTime)
            {
                NextWave();
            }
            else
            {
                data.waveTimer += Time.deltaTime;
            }
            healthDisplay.text = $"{System.Math.Round(data.health, 1)}/{System.Math.Round(data.maxHealth, 1)}";
        }
    }

    private void NextWave()
    {
        data.waveTimer -= data.waveTimer;
        for (int i = 0; i < 2 * data.wave + 1; i++)
        {
            SpawnNormalEnemy();
        }
        data.wave += 1;
        DisplayWave();
    }

    private void SpawnNormalEnemy()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnLocation = new Vector3(spawnDirection.x*data.spawnDistance, 0, spawnDirection.y*data.spawnDistance);
        BasicEnemy spawning = Instantiate(basicEnemy, spawnLocation, Quaternion.LookRotation(Tower.transform.position-spawnLocation,new Vector3(0,1,0)));
    }

    public void DisplayWave()
    {
        waveDisplay.text = $"wave: {data.wave}";
    }

    public void DealDamage(double damage)
    {
        data.health -= damage;
    }
}
