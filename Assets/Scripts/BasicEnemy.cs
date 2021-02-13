using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject Tower;
    public double speed = 5;
    public double dps = 1;
    public double hp = 3;
    public double moneyGiven = 1;

    private GameController gameController;
    private Data data;

    void Start()
    {
        data = Object.FindObjectOfType<Data>();
        gameController = Object.FindObjectOfType<GameController>();
    }

    void GrantRewards()
    {
        data.money += moneyGiven;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            GrantRewards();
            Destroy(this.gameObject);
        }
        float step = (float)speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, Tower.transform.position) > 1){
            transform.position = Vector3.MoveTowards(transform.position, Tower.transform.position, step);
        }
        else
        {
            print("hit-hit");
            gameController.DealDamage(dps * Time.deltaTime);
        }
    }
}
