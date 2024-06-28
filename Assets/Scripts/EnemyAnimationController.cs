using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private Vector2 lastDirection;
    private EnemyController enemyController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    public void UpdateMovementAnimation(Vector2 direction, float speed)
    {
        if (direction.magnitude > 0.1f)
        {
            lastDirection = direction;
            animator.SetFloat("Speed", speed);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        else
        {
            UpdateIdleAnimation();
        }
    }

    private void UpdateIdleAnimation()
    {
        if (enemyController.firing)
        {
            Vector2 directionToPlayer = (enemyController.player.position - transform.position).normalized;
            animator.SetFloat("Horizontal", directionToPlayer.x);
            animator.SetFloat("Vertical", directionToPlayer.y);
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Horizontal", lastDirection.x);
            animator.SetFloat("Vertical", lastDirection.y);
            animator.SetFloat("Speed", 0);
        }
    }
}
