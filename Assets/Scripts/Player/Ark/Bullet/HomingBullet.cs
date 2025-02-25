using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Father
{


    public float TrackingRadius = 10f;                 
    public ParticleSystem ExplosionEffect;           
    public float RotateSpeed = 200f;       
    public float Acceleration = 2f;        
    private Transform target;              // ��ǰ׷��Ŀ��
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
            // ���Ҿ��� "Enemy" ��ǩ�Ķ���
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy != null)
            {
                target = enemy.transform;
            }
        }

        if (target != null)
        {
            // ���㳯��Ŀ��ķ���?
            Direction = (target.position - transform.position).normalized;
            // �ƶ�׷�ٵ�
            transform.position += Direction * Speed * Time.deltaTime;
        }
        else
        {
            // ���û��Ŀ�꣬�����ص�ǰ�����ƶ�?
            transform.position += Direction * Speed * Time.deltaTime;
        }

        // �����������ڼ�����
        LifeTimeCounter += Time.deltaTime;
        if (LifeTimeCounter >= LifeTime)
        {
            // ����������ڽ��������ű�ըЧ�������ٶ���?
            PlayExplosionEffect();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����ײ�����Ƿ����˺�ͼ����?
        if (((1 << other.gameObject.layer) & damageLayers) != 0)
        {
            // ��Ŀ������˺�?
            
            PlayExplosionEffect();
            Destroy(gameObject);
        }
    }
}