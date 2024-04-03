using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Rigidbody2D rb;
    
    [Header("Attributes")] [SerializeField]
    private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private float basespeed;

    private float rotationSpeed = 500f;

    private void Start()
    {
        basespeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                // Decrease the player's health by 5
                LevelManager.main.DecreaseHealth(5);

                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;

        // Rotates the enemy towards their movement direction

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction != Vector2.zero)
        {
            float step = rotationSpeed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        }
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = basespeed;
    }
}
