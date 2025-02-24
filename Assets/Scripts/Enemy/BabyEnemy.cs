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
        IfProtect();
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
            Guard();
        }
        else
        {
            // ������·
            if (!isStaggered)
            {
                rb.velocity = Direction.normalized * Speed;
            }
        }
        if (Mom == null)
        {
            Destroy(gameObject);
        }
    }
    public void Guard()
    {
        if (Mom)
        {
            Vector2 direc = (PlayerTransform.position - Mom.transform.position).normalized;
            float angle = Mathf.Atan2(direc.y, direc.x) - 30 * Mathf.Deg2Rad;//������
            Vector3 Tarposition = new Vector3(Mom.transform.position.x + (Mathf.Cos(angle + (30 * Mathf.Deg2Rad * Mom.BabyGuard.IndexOf(gameObject))) * Radius), 
                Mom.transform.position.y + (Mathf.Sin(angle + (30 * Mathf.Deg2Rad * Mom.BabyGuard.IndexOf(gameObject))) * Radius), 0); // Ŀ��λ�� sin cos��Ҫ������
            transform.position = Vector3.Lerp(transform.position, Tarposition, 1 * Time.deltaTime);
        }
        else
        {
            rb.velocity = Direction.normalized * Speed;
        }
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

        rb.velocity = Vector2.zero;

        // �ȴ���ֱʱ�����
        yield return new WaitForSeconds(staggerDuration);

        // ������ֱ״̬
        isStaggered = false;        

    }

    private void OnDestroy()
    {
        Mom.BabyGuard.Remove(gameObject);
    }
}