using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Data data;
    [SerializeField] private StatsDisplay statsDisplay;
    public Data Data => data;

    public TMP_Text healthDisplay;
    public TMP_Text waveDisplay;
    public TMP_Text moneyDisplay;
    public BasicEnemy basicEnemy;
    public GameObject tower;
    public Slider healthBar;
    public GameObject ShopMenu;
    public GameObject SettingsMenu;
    [SerializeField] private Transform _dynamic;
    public Transform Dynamic => _dynamic;
    public Transform ShopContent;
    public UpgradeBuyingBar upgradeBuyingBar;

    public Dictionary<string, UpgradeBuyingBar> buyingBars = new Dictionary<string, UpgradeBuyingBar>() { };

    void Start()
    {
        HideMenus();
        GenerateBuyingBars();
    }

    void HideMenus()
    {
        ShopMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }

    void GenerateBuyingBars()
    {
        foreach (KeyValuePair<string, Data.Upgrade> entry in data.upgrades)
        {
            UpgradeBuyingBar buying = Instantiate(upgradeBuyingBar, ShopContent);
            buying.upgradeName = entry.Key;
            buyingBars.Add(entry.Key, buying);
            SetBuyingBarTexts(entry.Key);
            buying.GetComponentInChildren<ButtonBuyUpgrade>().gameController = this;
        }
        foreach (KeyValuePair<string, UpgradeBuyingBar> entry in buyingBars)
        {
            //print(entry.Key);
        }
        //print(buyingBars.Count);
    }

    private double Cost(string name)
    {
        return data.upgrades[name].upgradeBaseCost * System.Math.Pow(data.upgrades[name].upgradeMultCost, data.upgrades[name].upgradeLevel);
    }

    public void SetBuyingBarTexts(string name)
    {
        buyingBars[name].upgradeNameText.text = name;
        if (data.upgrades[name].upgradeMaxLevel == 0)
        {
            buyingBars[name].upgradeLevelsText.text = $"{data.upgrades[name].upgradeLevel}";
        }
        else
        {
            buyingBars[name].upgradeLevelsText.text = $"{data.upgrades[name].upgradeLevel}/{data.upgrades[name].upgradeMaxLevel}";
        }
        if (data.upgrades[name].upgradeMaxLevel > 0 && data.upgrades[name].upgradeLevel == data.upgrades[name].upgradeMaxLevel) //max level reached
        {
            if (!data.settings["Show maxed upgrades"])
            {
                buyingBars[name].gameObject.SetActive(false);
            }
            buyingBars[name].upgradeButton.SetActive(false);
        }
        else //can still be bought
        {
            buyingBars[name].upgradeButtonText.text = $"+1 level for {System.Math.Round(Cost(name), 2)}";
        }
    }

    public void BuyUpgrade(string name)
    {
        //print($"you tried to buy {name} upgrade");
        if (data.money >= Cost(name) && (data.upgrades[name].upgradeLevel < data.upgrades[name].upgradeMaxLevel || data.upgrades[name].upgradeMaxLevel == 0))
        {
            //print($"you have enough money to buy {name}");
            data.money -= Cost(name);
            data.upgrades[name].upgradeLevel += 1;
            SetBuyingBarTexts(name);
            if (name == "Damage")
            {
                statsDisplay.SetDamageDisplay();
            }
            if (name == "Attack speed")
            {
                statsDisplay.SetAttspdDisplay();
            }
            if (name == "Health")
            {
                data.health += data.healthPerUpgrade;
                data.maxHealth += data.healthPerUpgrade;
            }
        }
        else if (data.money < Cost(name))
        {
            //print($"not enough money to buy {name}!");
        }
        else
        {
            //print($"upgrade {name} already at max level!");
        }
    }

    void Update()
    {
        Regeneration();
        if (data.health <= 0)
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
        else
        {
            if (data.waveTimer >= data.waveTime)
            {
                StartCoroutine(NextWave());
            }
            else
            {
                data.waveTimer += Time.deltaTime;
            }
            healthDisplay.text = $"{System.Math.Round(data.health, 1)}/{System.Math.Round(data.maxHealth, 1)}";
            healthBar.value = (float)(data.health / data.maxHealth);
            moneyDisplay.text = $"Money: {System.Math.Round(data.money, 2)}";
        }
    }

    private void Regeneration()
    {
        data.health = System.Math.Min(data.maxHealth, data.health + data.hpRegenPerUpgrade * data.upgrades["Health regeneration"].upgradeLevel * Time.deltaTime);
    }

    IEnumerator NextWave()
    {
        data.wave += 1;
        DisplayWave();
        statsDisplay.SetWaveDisplay();
        data.waveTimer -= data.waveTimer;
        int waveEnemiesNumber = data.GetWaveEnemiesCount();
        for (int i = 0; i < waveEnemiesNumber; i++)
        {
            SpawnNormalEnemy();
            yield return new WaitForSeconds((float)data.waveDuration/(((float)waveEnemiesNumber) - 1));
        }
    }

    private void SpawnNormalEnemy()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnLocation = new Vector3(spawnDirection.x*data.spawnDistance, 0, spawnDirection.y*data.spawnDistance);
        BasicEnemy spawning = Instantiate(basicEnemy, spawnLocation, Quaternion.LookRotation(tower.transform.position-spawnLocation,new Vector3(0,1,0)), _dynamic);
        spawning.speed = data.GetWaveEnemiesSpeed();
        spawning.dps = data.GetWaveEnemiesDps();
        spawning.hp = data.GetWaveEnemiesHealth();
        spawning.moneyGiven = data.GetWaveEnemiesMoney();
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
        HideMenus();
        if (shouldBeShown)
        {
            menu.SetActive(true);
            //print($"menu shown: {menu}");
        }
        else
        {
            //print($"menu: {menu} is now hidden");
        }
    }

    public void ChangeVisibilityOfMaxedUpgrades(bool value)
    {
        data.settings["Show maxed upgrades"] = value;
        foreach (KeyValuePair<string, UpgradeBuyingBar> entry in buyingBars)
        {
            if(data.upgrades[entry.Key].upgradeMaxLevel > 0 && data.upgrades[entry.Key].upgradeLevel == data.upgrades[entry.Key].upgradeMaxLevel)
            {
                buyingBars[entry.Key].gameObject.SetActive(value);
            }
        }
    }

    public void ChangeVisibilityOfFloatingDamageText(bool value)
    {
        data.settings["Show floating damage text"] = value;
    }
}
