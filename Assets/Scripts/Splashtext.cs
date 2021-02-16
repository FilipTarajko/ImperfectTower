using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Splashtext : MonoBehaviour
{
    [SerializeField] private TMP_Text splashdisplay;
    [SerializeField] private TMP_Text splashshadow;
    string[] splashtexts = { "Minecraft's better", 
        "Also try Perfect Tower!",
        //2021 == bin 11111100101
        //02 == bin 10
        //12 == bin 1100
        "Since 11111100101-10-1100!",
        "Thanks, Marcin!",
        "This is main menu, if you haven't noticed"
    };



    void Start()
    {
        int chosen = Random.Range(0, splashtexts.Length);
        splashdisplay.text = splashtexts[chosen];
        splashshadow.text = splashtexts[chosen];
    }
}
