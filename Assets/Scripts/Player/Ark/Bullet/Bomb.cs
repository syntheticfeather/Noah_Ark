using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Father
{
    
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
            Instantiate(BloodSystem, transform.position, Quaternion.identity);
        }
        // 播放爆炸特效或音效
        PlayExplosionEffect();
        // 销毁炮弹
        Destroy(gameObject);
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