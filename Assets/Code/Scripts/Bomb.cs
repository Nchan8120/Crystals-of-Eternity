using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")] 
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private int explosionRange = 10;

    
    private Transform target;
    private Transform[] explodeTargets;
    private bool inExplodeRange;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetTarget(Transform _target, int dmg)
    {
        bulletDamage = dmg;
        target = _target;
    }
    
    private void FixedUpdate()
    {
        if (!target)
        {
            return;
        }
        
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        FindTarget();
        Destroy(gameObject);
        if (inExplodeRange)
        {
            Explode();
        }
    }
    
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRange, 
            (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            inExplodeRange = true;
            explodeTargets = new Transform[hits.Length];
        }
        else
        {
            inExplodeRange = false;
        }
        for (int i = 0; i < hits.Length; i++)
        {
            explodeTargets[i] = hits[i].transform;
        }
    }

    private void Explode()
    {
        for (int i = 0; i < explodeTargets.Length; i++)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            Bullet bulletScript = bulletObj.GetComponent<Bullet>();
            bulletScript.SetTarget(explodeTargets[i]);
        }
    }
}