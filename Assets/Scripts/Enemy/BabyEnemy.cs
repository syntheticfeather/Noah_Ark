using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyEnemy : MonoBehaviour
{
    // ������չ�߼�����ӻ���ĸ��
    public int Damage;
    public float Speed;
    public float Radius;
    private float Angle;


    public float KnockTime = 5;
    private float KnockCounter = 0;


    public float staggerDuration = 2f; // ��ֱ����ʱ��
    private bool isStaggered = false; // �Ƿ��ڽ�ֱ״̬

    public bool Protect = false;

    private Vector2 Direction;
    public MomEnemy Mom;   

    private Transform PlayerTransform;
    public Rigidbody2D rb;
    void Start()
    {
        PlayerTransform = PlayerHealthController.instance.transform;
        //IfProtect();
    }

    // Update is called once per frame
    void Update()
    {
        KnockCounter -= Time.deltaTime;

        //�������
        Direction = (PlayerTransform.position - transform.position).normalized;
        Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
        // Ӧ����ת
        transform.rotation = Quaternion.Euler(0, 0, Angle);

        if (Protect)
        {
            // ������·
            //Guard();
        }else
        {
            // ������·
            if (!isStaggered)
            {
                rb.velocity = Direction.normalized * Speed;
            }
        }
    }
    public void Guard()
    {
    }

    public void IfProtect()
    {
        if (Mom.BabyGuard.Count < Mom.NumOfGuard)
        {
            Protect = true;// ����С��
            Mom.BabyGuard.Add(gameObject); // �����б���
        }        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && KnockCounter < 0)
        {
            KnockCounter = KnockTime;           
            PlayerHealthController.instance.TakeDamage(Damage);
            StartStagger();
        }
    }
    void StartStagger()
    {
        if (!isStaggered)
        {
            StartCoroutine(StaggerCoroutine());
        }
    }

    IEnumerator StaggerCoroutine()
    {
        // ���뽩ֱ״̬
        isStaggered = true;
        Debug.Log("Monster is staggered!");

        rb.velocity = Vector2.zero;

        // �ȴ���ֱʱ�����
        yield return new WaitForSeconds(staggerDuration);

        // ������ֱ״̬
        isStaggered = false;
        Debug.Log("Monster is no longer staggered!");

    }

    private void OnDestroy()
    {
        Mom.BabyGuard.Remove(gameObject);
    }
}