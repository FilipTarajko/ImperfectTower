using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Data data;
    public TMP_Text damageDisplay;
    public TMP_Text attspdDisplay;
    public TMP_Text enemyDpsDisplay;
    public TMP_Text enemyHpDisplay;

    private void Start()
    {
        SetDamageDisplay();
        SetAttspdDisplay();
        SetWaveDisplay();
    }

    public void SetDamageDisplay()
    {
        damageDisplay.text = $"dmg: {data.GetBulletDamage()}";
    }
    
    public void SetAttspdDisplay()
    {
        attspdDisplay.text = $"{data.GetAttspd()} shots/s";
    }

    public void SetWaveDisplay()
    {
        enemyDpsDisplay.text = $"enemy dps: {data.GetWaveEnemiesDps()}";
        enemyHpDisplay.text = $"enemy hp: {data.GetWaveEnemiesHealth()}";
    }
}
