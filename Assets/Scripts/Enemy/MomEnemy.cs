using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MomEnemy : MonoBehaviour
{
    
    //召唤小怪
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
    public GameObject ColorSprite;// 指定位置变化形态
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
        //朝向玩家
        Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
        // 应用旋转
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
        // 检查是否达到后退距离
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerTransform.position);
        if (distanceToPlayer <= Radius)
        {
            toOrbit = false;
            rb.velocity = Vector2.zero;
            ColorSprite.GetComponent<SpriteRenderer>().color = Color.red;
        }else
        {
            // 后退到指定距离
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

