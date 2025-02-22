using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBomb : Father
{
    public int maxBounces = 3; // 最大反弹次数
    private int bounceCount = 0; // 当前反弹次数    
    void Start()
    {        
        LifeTimeCounter = LifeTime;
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Explode();
        }
        if (collision.name == "boss")
        {
            Boss.Instance.GetComponent<EnemyHealthController>().CurHealth -= ATK / 3;
            if (BloodSystem)
            Instantiate(BloodSystem, transform.position, Quaternion.identity);
        }

        // 检测是否碰到边界
        if (collision.tag == "Boundary")
        {
            if (bounceCount < maxBounces)
            {
                Bounce(collision);
                bounceCount++;
            }
            else
            {
                // 播放爆炸特效或音效
                PlayExplosionEffect();
                // 销毁炮弹
                Destroy(gameObject);
            }
        }
    }

    void Bounce(Collider2D collision)
    {
        // 获取碰撞边界的法线
        Vector2 normal = collision.transform.up;

        // 计算反射方向
        Direction = Vector2.Reflect(Direction, normal);

        // 更新炮弹的方向
        transform.up = Direction;
    }

    void Explode()
    {
        // 检测爆炸范围内的所有物体
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayers);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // 对物体应用伤害
            EnemyHealthController health = hitCollider.GetComponent<EnemyHealthController>();
            if (health != null)
            {
                health.TakeDamage(ATK);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        // 在场景中绘制爆炸范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
