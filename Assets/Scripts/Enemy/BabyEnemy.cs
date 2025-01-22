using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyEnemy : MonoBehaviour
{
    // ������չ�߼�����ӻ���ĸ��
    public int Damage;
    public float Speed;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Mom.CurOfGuard < Mom.NumOfGuard)
        {
            Protect = true;
        }
        Guard();
        KnockCounter -= Time.deltaTime;
        Direction = PlayerTransform.position - transform.position;
        //�������
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        // Ӧ����ת
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        if (!isStaggered)
        {
            rb.velocity = Direction.normalized * Speed;
        }
    }
    public void Guard()
    {
        if (Protect)
        {

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
}