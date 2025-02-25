using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDisEnemyController : MonoBehaviour
{
    //Զ�������
    public float Speed = 1;
    public float CircleSpeed;
    public float Radius;
    public int Damage;

    public float KnockTime = 5;
    private float KnockCounter = 0;

    private float Angle;
    private float Direc;
    private bool toOrbit = true; // �Ƿ����ں���
    private bool isCircling = false; // �Ƿ�������Ȧ
    private float Rate;// ת���ǲ�ת

    private Vector2 Direction;

    private Transform PlayerTransform;
    public Rigidbody2D rb;
    public GameObject bumb;
    
    void Start()
    {
        Rate = Random.Range(0f, 1f);        
        Radius += Random.Range(-2f, 3f);
        Direc = Random.Range(0,1);
        PlayerTransform = PlayerHealthController.instance.transform;
    }

    
    void Update()
    {
        KnockCounter -= Time.deltaTime;        
        Direction = PlayerTransform.position - transform.position;       
        //�������
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
        // Ӧ����ת
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (KnockCounter < 0)
        {
            bumb.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(bumb, transform.position, bumb.transform.rotation).gameObject.SetActive(true);
            KnockCounter = KnockTime;
        }
        Movement();
    }
    private void Movement()
    {

        if (KnockCounter > 0)
        {
            if (toOrbit)
            {
                ToOrbit();
            }
            else if (isCircling)
            {
                CirclePlayer();
            }
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
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), .5f * Time.deltaTime);
    }
    public void ToOrbit()
    {

        // ����Ƿ�ﵽ���˾���
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerTransform.position);
        if (distanceToPlayer <= Radius)
        {
            if (Rate >= 0.3)
            {
                toOrbit = false;
                isCircling = true;
            }            
            // ��ʼ��Բ���˶��ĽǶ�
            Vector2 direction = transform.position - PlayerTransform.position;
            Angle = Mathf.Atan2(direction.y, direction.x);
        }
        else
        {
            // ���˵�ָ������
            transform.position += (Vector3)Direction * Speed * Time.deltaTime;
        }
    }
}
