using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HomingBullet : Father
{
    public float Speed;    
    public float TrackingRadius = 10f;                 
    public ParticleSystem ExplosionEffect;           
    public float RotateSpeed = 200f;       
    public float Acceleration = 2f;        
    private Transform target;              // 当前追踪目标
    private Rigidbody2D rb;
    private float currentSpeed;

    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = Speed;
        LifeTimeCounter = LifeTime;
    }

    void Update()
    {
        LifeTimeCounter -= Time.deltaTime;
        if (LifeTimeCounter <= 0)
        {
            Explode();
            return;
        }

        FindTarget();
        MoveTowardsTarget();
    }

    void FindTarget()
    {
        // 如果已经有目标且目标在范围内，则保持追踪
        if (target != null && Vector2.Distance(transform.position, target.position) <= TrackingRadius)
        {
            return;
        }

        // 搜索范围内最近的敌人
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, TrackingRadius, damageLayers);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.transform;
                }
            }
        }

        target = closestTarget;
    }

    void MoveTowardsTarget()
    {
        if (target == null)
        {            
            rb.velocity = transform.up * currentSpeed;
            return;
        }

        
        Vector2 direction = (target.position - transform.position).normalized;

        
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        
        rb.angularVelocity = -rotateAmount * RotateSpeed;

        
        currentSpeed += Acceleration * Time.deltaTime;
        rb.velocity = transform.up * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // 播放爆炸特效
        if (ExplosionEffect != null)
        {
            Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        }        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayers);
        foreach (Collider2D hitCollider in hitColliders)
        {
            
            EnemyHealthController health = hitCollider.GetComponent<EnemyHealthController>();
            if (health != null)
            {
                health.TakeDamage(ATK);
            }
        }        
        Destroy(gameObject);
    }

    
}