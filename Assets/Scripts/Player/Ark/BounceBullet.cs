using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBomb : MonoBehaviour
{
    public GameObject artillery;
    public float Speed;
    public float Decration;
    public float ChangeRate; // 动画

    public float explosionRadius = 5f; // 爆炸范围
    public int ATK;

    public float LifeTime;
    private float LifeTimeCounter;

    private Vector3 Direction;
    public LayerMask damageLayers; // 可以受到伤害的图层
    public ParticleSystem ParticleSystem;
    public ParticleSystem BloodSystem;

    public int maxBounces = 3; // 最大反弹次数
    private int bounceCount = 0; // 当前反弹次数

    void Start()
    {
        LifeTimeCounter = LifeTime;
        Direction = artillery.transform.up;
    }

    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;
        if (Speed > 0)
        {
            Speed -= Time.deltaTime * Decration;
        }
        if (Speed <= 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, ChangeRate);
            if (transform.localScale == Vector3.zero)
            {
                Destroy(gameObject);
            }
        }
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

    void PlayExplosionEffect()
    {
        // 音效暂定
        Instantiate(ParticleSystem, transform.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        // 在场景中绘制爆炸范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
