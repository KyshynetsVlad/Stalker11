using UnityEngine;

public class PredictiveChase : MonoBehaviour
{
    private EnemyController enemyController;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public void Chase(Transform player, float chaseSpeed, float stoppingDistance)
    {
        Vector2 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        Vector2 predictedPosition = (Vector2)player.position + playerVelocity * 2.0f; // 2.0f - фактор предсказания

        float distanceToPredictedPosition = Vector2.Distance(transform.position, predictedPosition);

        if (distanceToPredictedPosition > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, predictedPosition, chaseSpeed * Time.deltaTime);
        }
        else
        {
            enemyController.currentState = EnemyController.State.Attacking;
        }
    }
}