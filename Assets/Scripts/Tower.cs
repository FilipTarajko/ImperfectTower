using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Data data;
    public GameObject target;
    public GameObject bulletSpawner;
    public GameObject bulletPrefab;

    void Start()
    {
        data = Object.FindObjectOfType<Data>();
    }

    void TargetEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        double shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = target;
        foreach(GameObject enemy in enemies)
        {
            double distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= data.towerRange)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    void Shoot(GameObject target)
    {
        print($"Shooting {target}");
        GameObject shotBulletGO = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
        shotBulletGO.GetComponent<Bullet>().target = target;
    }

    void Update()
    {
        if (data.shootTimer < data.shootTime)
        {
            data.shootTimer += Time.deltaTime;
        }
        if (data.shootTimer >= data.shootTime)
        {
            TargetEnemy();
            if (target != null)
            {
                data.shootTimer -= data.shootTime;
                Shoot(target);
            }
        }
    }
}
