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
        // ���ű�ը��Ч����Ч
        PlayExplosionEffect();
        // �����ڵ�
        Destroy(gameObject);
        //Debug.Log("����ײ������");
    }
    void OnDrawGizmosSelected()
    {
        // �ڳ����л��Ʊ�ը��Χ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}