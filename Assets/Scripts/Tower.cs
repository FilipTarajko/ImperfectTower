using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Data data;
    private Transform _dynamic;
    [SerializeField] private GameController gameController;
    public GameObject target;
    public GameObject bulletSpawner;
    public GameObject bulletPrefab;

    void Start()
    {
        data = gameController.Data;
        _dynamic = gameController.Dynamic;
    }

    void TargetEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        double shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = target;
        foreach(GameObject enemy in enemies)
        {
            double distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy + 0.1 < shortestDistance)
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
        //print($"Shooting {target}");
        GameObject shotBulletGO = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation, _dynamic);
        Bullet shotBullet = shotBulletGO.GetComponent<Bullet>();
        shotBullet.target = target;
        shotBullet.dmg = data.bulletBaseDamage + data.upgrades[0].upgradeLevel * data.dmgPerUpgrade; //temp?
        shotBullet.speed = data.bulletSpeed;
    }

    void Update()
    {
        double shootCd = data.shootTime / System.Math.Pow(data.attspdPerUpgradeMult, data.upgrades[1].upgradeLevel);
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
