using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //  ������ң���սײ����Զ�뻻λ
    //  �������һ��λ�ã����У�Զ�̹�����
    // <><><> ��ս��һ������ֹͣ
    public int kind;

    public float Speed = 5;
    public float CircleSpeed;
    public float Radius;
    public int Damage;

    public float KnockTime = 5;
    private float KnockCounter = 0;

    private float Angle;
    private float Direc;

    private bool isRetreating = false; // �Ƿ����ں���
    private bool isCircling = false; // �Ƿ�������Ȧ

    private Vector2 Direction;

    private Transform PlayerTransform;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {       
        PlayerTransform = FindAnyObjectByType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        KnockCounter-=Time.deltaTime;
        Direction = PlayerTransform.position - transform.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        // Ӧ����ת
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        if (KnockCounter < 0)
        {
            isRetreating = false;
            isCircling = false;
            rb.velocity = Direction.normalized * Speed;

        }
        Movement(KnockCounter);        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && KnockCounter < 0)
        {
            KnockCounter = KnockTime;
            // ���������߼�
            isRetreating = true;
            isCircling = false;
            Direc = Random.value;
            PlayerHealthContler.instance.TakeDamage(Damage);
        }
    }






    private void Movement(float KnockCounter)
    {

        if (KnockCounter > 0)
        {
            if (isRetreating)
            {
                Retreat();
            }
            else if (isCircling)
            {
                CirclePlayer();
            }
        }
    }
    void Retreat()
    {
        // ���˵�ָ������
        transform.position += -(Vector3)Direction * Speed * Time.deltaTime;

        // ����Ƿ�ﵽ���˾���
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerTransform.position);
        if (distanceToPlayer >= Radius)
        {
            isRetreating = false;
            isCircling = true;

            // ��ʼ��Բ���˶��ĽǶ�
            Vector2 direction = transform.position - PlayerTransform.position;
            Angle = Mathf.Atan2(direction.y, direction.x);
        }
    }
    void CirclePlayer()
    {
        // ���½Ƕ�
        if (Direc >= 0.5)
        {
            Angle += CircleSpeed * Time.deltaTime;
        }
        else
        {
            Angle -= CircleSpeed * Time.deltaTime;
        }

        // ������λ��
        float x = PlayerTransform.position.x + Radius * Mathf.Cos(Angle);
        float y = PlayerTransform.position.y + Radius * Mathf.Sin(Angle);

        // ���µ���λ��        
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), 0.5f);
    }
}
