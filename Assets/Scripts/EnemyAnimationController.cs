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

    public void UpdateMovementAnimation(Vector2 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            lastDirection = direction;
            if (direction.x > 0)
                animator.Play("Move_Right");
            else if (direction.x < 0)
                animator.Play("Move_Left");
            else if (direction.y > 0)
                animator.Play("Move_Up");
            else if (direction.y < 0)
                animator.Play("Move_Down");
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
            if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
            {
                if (directionToPlayer.x > 0)
                    animator.Play("Idle_Right");
                else
                    animator.Play("Idle_Left");
            }
            else
            {
                if (directionToPlayer.y > 0)
                    animator.Play("Idle_Up");
                else
                    animator.Play("Idle_Down");
            }
        }
        else
        {
            if (lastDirection.x > 0)
                animator.Play("Idle_Right");
            else if (lastDirection.x < 0)
                animator.Play("Idle_Left");
            else if (lastDirection.y > 0)
                animator.Play("Idle_Up");
            else if (lastDirection.y < 0)
                animator.Play("Idle_Down");
        }
    }
}
