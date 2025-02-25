using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDisEnemyController : MonoBehaviour
{
    //远距离击打
    public float Speed = 1;
    public float CircleSpeed;
    public float Radius;
    public int Damage;

    public float KnockTime = 5;
    private float KnockCounter = 0;

    private float Angle;
    private float Direc;
    private bool toOrbit = true; // 是否正在后退
    private bool isCircling = false; // 是否正在绕圈
    private float Rate;// 转还是不转

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
        //朝向玩家
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
        // 应用旋转
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
        // 更新角度
        if (Direc >= 0.5)
        {
            Angle += CircleSpeed * Time.deltaTime;
        }
        else
        {
            Angle -= CircleSpeed * Time.deltaTime;
        }

        // 计算新位置
        float x = PlayerTransform.position.x + Radius * Mathf.Cos(Angle);
        float y = PlayerTransform.position.y + Radius * Mathf.Sin(Angle);

        // 更新敌人位置        
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), .5f * Time.deltaTime);
    }
    public void ToOrbit()
    {

        // 检查是否达到后退距离
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerTransform.position);
        if (distanceToPlayer <= Radius)
        {
            if (Rate >= 0.3)
            {
                toOrbit = false;
                isCircling = true;
            }            
            // 初始化圆周运动的角度
            Vector2 direction = transform.position - PlayerTransform.position;
            Angle = Mathf.Atan2(direction.y, direction.x);
        }
        else
        {
            // 后退到指定距离
            transform.position += (Vector3)Direction * Speed * Time.deltaTime;
        }
    }
}
