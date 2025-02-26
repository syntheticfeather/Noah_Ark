using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBomb : Father
{
    
    public int maxBounces = 3; // 最大反弹次数
    private int bounceCount = 0; // 当前反弹次数

    private Camera mainCamera;
    private float screenLeft, screenRight, screenTop, screenBottom;

    void Start()
    {
        LifeTimeCounter = LifeTime;

        // 获取主摄像机
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        // 计算屏幕边界
        CalculateScreenBounds();
    }

    void Update()
    {
        // 移动炮弹
        transform.Translate(Direction * Time.deltaTime * Speed);

        // 检测是否碰到屏幕边缘
        CheckScreenBounds();
    }

    void CalculateScreenBounds()
    {
        
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // 获取屏幕的左右上下边界
        screenLeft = bottomLeft.x;
        screenRight = topRight.x;
        screenBottom = bottomLeft.y;
        screenTop = topRight.y;

        //Debug.Log($"Screen Bounds: Left={screenLeft}, Right={screenRight}, Bottom={screenBottom}, Top={screenTop}");
    }

    void CheckScreenBounds()
    {
        // 检测是否碰到左右边缘
        if (transform.position.x <= screenLeft || transform.position.x >= screenRight)
        {
            HandleBounce();
        }

        // 检测是否碰到上下边缘
        if (transform.position.y <= screenBottom || transform.position.y >= screenTop)
        {
            HandleBounce();
        }
    }

    void HandleBounce()
    {
        if (bounceCount < maxBounces)
        {
            // 反弹逻辑
            if (transform.position.x <= screenLeft || transform.position.x >= screenRight)
            {
                Debug.Log("Bouncing on X axis");
                // 反转 x 方向（左右反弹）
                Direction = new Vector2(-Direction.x, Direction.y);
            }
            else if (transform.position.y <= screenBottom || transform.position.y >= screenTop)
            {
                Debug.Log("Bouncing on Y axis");
                // 反转 y 方向（上下反弹）
                Direction = new Vector2(Direction.x, -Direction.y);
            }

            bounceCount++; // 增加反弹次数
        }
        else
        {
            // 达到最大反弹次数，销毁炮弹
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