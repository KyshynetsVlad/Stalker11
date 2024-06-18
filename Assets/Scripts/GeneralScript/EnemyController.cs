using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float stoppingDistance = 3f;
    public float retreatDistance = 2f;
    public float shootingRange = 5f;
    public float enemyFireRate = 0.5f;
    public float enemyVision = 6f;
    public GameObject enemyWeapon;
    private Transform player;
    private float lastShotTime;
    Weapon weapon;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found in the scene.");
            return;
        }

        // ?????????????? Weapon ? ????? ???????
        if (enemyWeapon != null)
        {
            WeaponDataEnemy weaponData = enemyWeapon.GetComponent<WeaponDataEnemy>();

            if (weaponData != null)
            {
                weapon = weaponData.GetWeapon();
            }
            else
            {
                Debug.LogError("WeaponData component not found on weaponPrefab.");
            }
        }
        else
        {
            Debug.LogError("Weapon prefab is not assigned.");
        }
    }

    private void Update()
    {
        if (player == null || weapon == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ?????????????? ??????
        if (distanceToPlayer > stoppingDistance&& distanceToPlayer < enemyVision)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        // ?????? ??? ??????, ???? ??? ??????? ???????
        else if (distanceToPlayer < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
        }

        // ???????? ?? ??????, ???? ??? ? ????? ??????????
        if (distanceToPlayer <= shootingRange && Time.time > lastShotTime + enemyFireRate)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    void Shoot()
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon is not set.");
            return;
        }
        GameObject bullet = Instantiate(weapon.bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // ????????? ???????? ???????? ? ??? ??????
            Vector2 shootDirection = (player.position - transform.position).normalized;

            bulletScript.SetWeapon(weapon);
            bulletScript.velocity = shootDirection * 10f;
            bulletScript.player = this.gameObject;

            // ????????? ???? ? ???????? ????????
            bullet.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);

            // ?????? ???? ????? 2 ???????
            Destroy(bullet, 2f);

            lastShotTime = Time.time;
        }
    }
}