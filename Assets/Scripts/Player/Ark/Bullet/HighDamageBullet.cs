using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDamageBullet : Father
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
            collision.GetComponent<EnemyHealthController>().TakeDamage(ATK);
            if (BloodSystem)
            Instantiate(BloodSystem, transform.position, Quaternion.identity);
        }
        if (collision.name == "boss")
        {
            Boss.Instance.GetComponent<EnemyHealthController>().CurHealth -= ATK / 3;
            if (BloodSystem)
            Instantiate(BloodSystem, transform.position, Quaternion.identity);
        }
        // 播放爆炸特效或音效
        PlayExplosionEffect();
        // 销毁炮弹
        Destroy(gameObject);
        //Debug.Log("因碰撞而销毁");
    }
    void OnDrawGizmosSelected()
    {
        // 在场景中绘制爆炸范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}