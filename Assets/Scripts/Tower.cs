using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Data data;
    private Transform _dynamic;
    [SerializeField] private GameController gameController;
    public BasicEnemy target;
    public GameObject bulletSpawner;
    public GameObject bulletPrefab;

    void Start()
    {
        data = gameController.Data;
        _dynamic = gameController.Dynamic;
    }

    void TargetEnemy()
    {
        BasicEnemy[] enemies = FindObjectsOfType<BasicEnemy>();
        double shortestDistance = Mathf.Infinity;
        BasicEnemy nearestEnemy = target;
        foreach(BasicEnemy enemy in enemies)
        {
            double distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy + 0.1 < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= data.towerRange+data.attRangePerUpgrade*data.upgrades["Attack range"].upgradeLevel)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    void Shoot(BasicEnemy target)
    {
        //print($"Shooting {target}");
        GameObject shotBulletGO = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation, _dynamic);
        Bullet shotBullet = shotBulletGO.GetComponent<Bullet>();
        shotBullet.target = target;
        shotBullet.dmg = data.GetBulletDamage(); //temp?
        shotBullet.speed = data.bulletSpeed * (float)data.GetAttspd();
    }

    

    void Update()
    {
        double shootCd = 1/data.GetAttspd();
        if (data.shootTimer < shootCd)
        {
            data.shootTimer += Time.deltaTime;
        }
        if (data.shootTimer >= shootCd)
        {
            TargetEnemy();
            if (target != null)
            {
                data.shootTimer -= shootCd;
                Shoot(target);
            }
        }
    }
}
