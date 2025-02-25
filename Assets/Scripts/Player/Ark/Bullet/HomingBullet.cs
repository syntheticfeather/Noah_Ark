using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Father
{

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


    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            // 查找具有 "Enemy" 标签的对象
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy != null)
            {
                target = enemy.transform;
            }
        }

        if (target != null)
        {
            // 计算朝向目标的方向
            Direction = (target.position - transform.position).normalized;
            // 移动追踪弹
            transform.position += Direction * Speed * Time.deltaTime;
        }
        else
        {
            // 如果没有目标，继续沿当前方向移动
            transform.position += Direction * Speed * Time.deltaTime;
        }

        // 更新生命周期计数器
        LifeTimeCounter += Time.deltaTime;
        if (LifeTimeCounter >= LifeTime)
        {
            // 如果生命周期结束，播放爆炸效果并销毁对象
            PlayExplosionEffect();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞对象是否在伤害图层中
        if (((1 << other.gameObject.layer) & damageLayers) != 0)
        {
            // 对目标造成伤害
            
            PlayExplosionEffect();
            Destroy(gameObject);
        }
    }
}