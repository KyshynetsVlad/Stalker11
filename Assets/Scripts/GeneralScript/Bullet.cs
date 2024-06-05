using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0, 0);
    public GameObject player;
    private Weapon currentWeapon;
    private float damage;

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        damage = currentWeapon.damage;
    }

    void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        Debug.DrawLine(currentPosition, newPosition, Color.red);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            if (other != player)
            {
                if (other.CompareTag("Enemy"))
                {
                    Health health = other.GetComponent<Health>();
                    if (health != null)
                    {
                        health.TakeDamage(damage);
                    }
                    Destroy(gameObject);
                    break;
                }
                if (other.CompareTag("Player"))
                {
                    Health health = other.GetComponent<Health>();
                    if (health != null)
                    {
                        health.TakeDamage(damage);
                    }
                    Destroy(gameObject);
                    break;
                }
                /*    if (other.CompareTag("Empty"))
                    {
                        Destroy(gameObject);
                        break;
                    }*/
            }
        }
        transform.position = newPosition;
    }
}
