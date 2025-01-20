using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
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
        Explode();
    }


    void Explode()
    {
        // 检测爆炸范围内的所有物体
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayers);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // 计算物体与爆炸中心的距离
            float distance = Vector2.Distance(transform.position, hitCollider.transform.position);

            // 根据距离计算伤害（距离越远，伤害越小）
            float damage = ATK * (1 - distance / explosionRadius);

            // 对物体应用伤害
            EnemyHealthController health = hitCollider.GetComponent<EnemyHealthController>();
            if (health != null)
            {
                health.TakeDamage(ATK);
            }
        }

        // 播放爆炸特效或音效
        PlayExplosionEffect();

        // 销毁炮弹
        Destroy(gameObject);
    }

    void PlayExplosionEffect()
    {
        Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        // 音效暂定
    }

    void OnDrawGizmosSelected()
    {
        // 在场景中绘制爆炸范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}