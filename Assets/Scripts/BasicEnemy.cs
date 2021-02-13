using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject Tower;
    private float speed = 5;
    private double dps = 1;
    public double hp = 3;

    private GameController gameController;

    void Start()
    {
        gameController = Object.FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(this);
        }
        float step = speed * Time.deltaTime;
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
