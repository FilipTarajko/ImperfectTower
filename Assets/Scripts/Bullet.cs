using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public double dmg;

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
                target.GetComponent<BasicEnemy>().hp -= dmg;
                //Destroy(target); //temp
                Destroy(this.gameObject);
            }
        }
    }
}
