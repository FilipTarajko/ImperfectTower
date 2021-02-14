using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject Tower;
    public double speed;
    public double dps;
    public double hp;
    public double moneyGiven;

    private GameController gameController;
    private Data data;

    public void SetData(Data data)
    {
        this.data = data;
    }
    public void SetGameController(GameController gameController)
    {
        this.gameController = gameController;
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
