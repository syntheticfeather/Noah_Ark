using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBomb : Father
{
    
    public int maxBounces = 3; // ��󷴵�����
    private int bounceCount = 0; // ��ǰ��������

    private Camera mainCamera;
    private float screenLeft, screenRight, screenTop, screenBottom;

    void Start()
    {
        LifeTimeCounter = LifeTime;

        // ��ȡ�������
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        // ������Ļ�߽�
        CalculateScreenBounds();
    }

    void Update()
    {
        // �ƶ��ڵ�
        transform.Translate(Direction * Time.deltaTime * Speed);

        // ����Ƿ�������Ļ��Ե
        CheckScreenBounds();
    }

    void CalculateScreenBounds()
    {
        
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // ��ȡ��Ļ���������±߽�
        screenLeft = bottomLeft.x;
        screenRight = topRight.x;
        screenBottom = bottomLeft.y;
        screenTop = topRight.y;

        //Debug.Log($"Screen Bounds: Left={screenLeft}, Right={screenRight}, Bottom={screenBottom}, Top={screenTop}");
    }

    void CheckScreenBounds()
    {
        // ����Ƿ��������ұ�Ե
        if (transform.position.x <= screenLeft || transform.position.x >= screenRight)
        {
            HandleBounce();
        }

        // ����Ƿ��������±�Ե
        if (transform.position.y <= screenBottom || transform.position.y >= screenTop)
        {
            HandleBounce();
        }
    }

    void HandleBounce()
    {
        if (bounceCount < maxBounces)
        {
            // �����߼�
            if (transform.position.x <= screenLeft || transform.position.x >= screenRight)
            {
                Debug.Log("Bouncing on X axis");
                // ��ת x �������ҷ�����
                Direction = new Vector2(-Direction.x, Direction.y);
            }
            else if (transform.position.y <= screenBottom || transform.position.y >= screenTop)
            {
                Debug.Log("Bouncing on Y axis");
                // ��ת y �������·�����
                Direction = new Vector2(Direction.x, -Direction.y);
            }

            bounceCount++; // ���ӷ�������
        }
        else
        {
            // �ﵽ��󷴵������������ڵ�
            Debug.Log("Max bounces reached. Destroying bomb.");
            Destroy(gameObject);
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
            if (BloodSystem)
                Instantiate(BloodSystem, transform.position, Quaternion.identity);
        }

        // ���ű�ը��Ч����Ч
        PlayExplosionEffect();
        // �����ڵ�
        Destroy(gameObject);
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