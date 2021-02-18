using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BasicEnemy target;
    public float speed;
    public double dmg;

    private void Start()
    {
        FloatingTextController.Initialize();
    }

    private void Update()
    {
        if (target.Equals(null))
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            }
            else
            {
                target.TakeDamage(dmg);
                Destroy(this.gameObject);
            }
        }
    }
}
