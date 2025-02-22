using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBomb : Father
{
    public int maxBounces = 3; // ��󷴵�����
    private int bounceCount = 0; // ��ǰ��������    
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

        // ����Ƿ������߽�
        if (collision.tag == "Boundary")
        {
            if (bounceCount < maxBounces)
            {
                Bounce(collision);
                bounceCount++;
            }
            else
            {
                // ���ű�ը��Ч����Ч
                PlayExplosionEffect();
                // �����ڵ�
                Destroy(gameObject);
            }
        }
    }

    void Bounce(Collider2D collision)
    {
        // ��ȡ��ײ�߽�ķ���
        Vector2 normal = collision.transform.up;

        // ���㷴�䷽��
        Direction = Vector2.Reflect(Direction, normal);

        // �����ڵ��ķ���
        transform.up = Direction;
    }

    void Explode()
    {
        // ��ⱬը��Χ�ڵ���������
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayers);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // ������Ӧ���˺�
            EnemyHealthController health = hitCollider.GetComponent<EnemyHealthController>();
            if (health != null)
            {
                health.TakeDamage(ATK);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        // �ڳ����л��Ʊ�ը��Χ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
