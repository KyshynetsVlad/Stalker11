using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : NPCHP
{
    public float moveSpeed = 0.5f;
    public float stoppingDistance = 3f;
    public float retreatDistance = 2f;
    public float shootingRange = 5f;
    public float enemyFireRate = 0.5f;
    public float enemyVision = 6f;
    public float patrolSpeed = 0.2f;
    public float chaseSpeed = 0.5f;
    public BoxCollider2D patrolArea;
    public int health = 100;
    public GameObject enemyWeapon;
    public GameObject[] lootItems;
    public bool firing = false;

    public enum ChaseStrategy { Direct, Predictive }
    public ChaseStrategy chaseStrategy;

    internal Transform player;
    private float lastShotTime;
    private Vector2 patrolPoint;
    private EnemyAnimationController animationController;
    private Weapon weapon;

    public enum State { Patrolling, Chasing, Attacking, Returning }
    public State currentState;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animationController = GetComponent<EnemyAnimationController>();

        if (player == null)
        {
            Debug.LogError("Player not found in the scene.");
            return;
        }

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

        if (patrolArea == null)
        {
            Debug.LogError("Patrol area is not assigned.");
            return;
        }

        patrolPoint = GetRandomPatrolPoint();
        currentState = State.Patrolling;

        // Случайный выбор стратегии преследования
        chaseStrategy = (ChaseStrategy)Random.Range(0, System.Enum.GetValues(typeof(ChaseStrategy)).Length);
    }

    private void Update()
    {
        if (player == null || weapon == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                if (distanceToPlayer < enemyVision)
                {
                    currentState = State.Chasing;
                }
                break;
            case State.Chasing:
                ChasePlayer(distanceToPlayer);
                if (distanceToPlayer > enemyVision)
                {
                    currentState = State.Returning;
                }
                break;
            case State.Attacking:
                AttackPlayer(distanceToPlayer);
                if (distanceToPlayer > shootingRange)
                {
                    currentState = State.Chasing;
                }
                break;
            case State.Returning:
                ReturnToPatrol();
                if (Vector2.Distance(transform.position, patrolPoint) < 1f)
                {
                    currentState = State.Patrolling;
                }
                break;
        }
    }

    private void Patrol()
    {
        if (Vector2.Distance(transform.position, patrolPoint) < 1f)
        {
            patrolPoint = GetRandomPatrolPoint();
        }
        else
        {
            MoveTowards(patrolPoint, patrolSpeed);
        }
    }

    private Vector2 GetRandomPatrolPoint()
    {
        Bounds bounds = patrolArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }

    private void ChasePlayer(float distanceToPlayer)
    {
        switch (chaseStrategy)
        {
            case ChaseStrategy.Direct:
                if (distanceToPlayer > stoppingDistance && distanceToPlayer < enemyVision)
                {
                    MoveTowards(player.position, chaseSpeed);
                }
                else if (distanceToPlayer <= stoppingDistance)
                {
                    currentState = State.Attacking;
                }
                break;

            case ChaseStrategy.Predictive:
                PredictiveChase predictiveChase = GetComponent<PredictiveChase>();
                if (predictiveChase != null)
                {
                    predictiveChase.enabled = true;
                    predictiveChase.Chase(player, chaseSpeed, stoppingDistance);
                }
                break;
        }
    }

    private void AttackPlayer(float distanceToPlayer)
    {
        firing = true;
        if (distanceToPlayer <= shootingRange && Time.time > lastShotTime + enemyFireRate)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    private void ReturnToPatrol()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < enemyVision)
        {
            currentState = State.Chasing;
        }
        else
        {
            MoveTowards(patrolPoint, patrolSpeed);
        }
    }

    private void MoveTowards(Vector2 target, float speed)
    {
        Vector2 currentPosition = transform.position;
        Vector2 direction = (target - currentPosition).normalized;
        animationController.UpdateMovementAnimation(direction, speed);
        transform.position = Vector2.MoveTowards(currentPosition, target, speed * Time.deltaTime);
    }

    private void Shoot()
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
            Vector2 shootDirection = (player.position - transform.position).normalized;

            bulletScript.SetWeapon(weapon);
            bulletScript.velocity = shootDirection * 10f;
            bulletScript.player = this.gameObject;

            bullet.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);

            Destroy(bullet, 2f);

            lastShotTime = Time.time;
        }
    }
}
