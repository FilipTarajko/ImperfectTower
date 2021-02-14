using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Data data;
    public Data Data => data;

    public TMP_Text healthDisplay;
    public TMP_Text waveDisplay;
    public TMP_Text moneyDisplay;
    public BasicEnemy basicEnemy;
    public GameObject tower;
    public Slider healthBar;
    public GameObject ShopMenu;
    [SerializeField] private Transform _dynamic;
    public Transform Dynamic => _dynamic;


    void Start()
    {
        ShopMenu.SetActive(false);
    }

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
            healthBar.value = (float)(data.health / data.maxHealth);
            moneyDisplay.text = $"Money: {System.Math.Round(data.money, 1)}";
        }
    }

    private void NextWave()
    {
        data.wave += 1;
        data.waveTimer -= data.waveTimer;
        for (int i = 0; i < data.wave + 3; i++)
        {
            SpawnNormalEnemy();
        }
        DisplayWave();
    }

    private void SpawnNormalEnemy()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnLocation = new Vector3(spawnDirection.x*data.spawnDistance, 0, spawnDirection.y*data.spawnDistance);
        BasicEnemy spawning = Instantiate(basicEnemy, spawnLocation, Quaternion.LookRotation(tower.transform.position-spawnLocation,new Vector3(0,1,0)), _dynamic);
        spawning.speed = 4+data.wave/3;
        spawning.dps = 1+data.wave/10;
        spawning.hp = 2+data.wave;
        spawning.moneyGiven = data.wave;
        spawning.SetData(data);
        spawning.SetGameController(this);
    }

    public void DisplayWave()
    {
        waveDisplay.text = $"Wave: {data.wave}";
    }

    public void DealDamage(double damage)
    {
        data.health -= damage;
    }

    public void ShowMenu(GameObject menu)
    {
        bool shouldBeShown = true;
        if (menu.activeInHierarchy)
        {
            shouldBeShown = false;
        }
        ShopMenu.SetActive(false);
        if (shouldBeShown)
        {
            menu.SetActive(true);
            print($"menu shown: {menu}");
        }
        else
        {
            print($"menu: {menu} is now hidden");
        }
    }
}
