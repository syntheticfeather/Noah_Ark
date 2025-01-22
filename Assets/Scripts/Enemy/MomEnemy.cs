using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MomEnemy : MonoBehaviour
{
    
    //�ٻ�С��
    public float Speed = 1;    
    public float Radius;
    public int NumOfGuard = 3;    

    public float KnockTime = 5;
    private float KnockCounter = 0;

    private bool toOrbit = true;
    private float Angle;

    public Vector2 Direction;

    private Transform PlayerTransform;
    public Rigidbody2D rb;

    public List<GameObject> BabyGuard = new List<GameObject>();
    public GameObject ColorSprite;// ָ��λ�ñ仯��̬
    public GameObject BabyEnemy;

    void Start()
    {
        Radius += Random.Range(-2f, 3f);
        PlayerTransform = PlayerHealthController.instance.transform;        
    }


    void Update()
    {
        KnockCounter -= Time.deltaTime;
        Direction = PlayerTransform.position - transform.position;
        //�������
        Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
        // Ӧ����ת
        transform.rotation = Quaternion.Euler(0, 0, Angle);
        if (KnockCounter < 0)
        {
            Generate();
        }
        Movement();
    }
    private void Movement()
    {
        if (toOrbit)
        {
            ToOrbit();
        }        
    }
    public void ToOrbit()
    {
        // ����Ƿ�ﵽ���˾���
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerTransform.position);
        if (distanceToPlayer <= Radius)
        {
            toOrbit = false;
            rb.velocity = Vector2.zero;
            ColorSprite.GetComponent<SpriteRenderer>().color = Color.red;
        }else
        {
            // ���˵�ָ������
            rb.velocity = Direction.normalized * Speed;
        }
    }

    public void Generate()
    {
        for (int i = 0; i < 3; i++)
        {
            BabyEnemy.transform.rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
            
            Instantiate(BabyEnemy, transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(3f, 3f), 0), BabyEnemy.transform.rotation, transform).gameObject.SetActive(true);
        }
        KnockCounter = KnockTime;
    }

}

