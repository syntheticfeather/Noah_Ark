using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : MonoBehaviour
{
    //  跟踪玩家，近战撞击，远离换位
    //  距离玩家一定位置，绕行，远程攻击。
    // <><><> 近战到一定距离停止

    public float Speed = 5;
    public float CircleSpeed;
    public float Radius;
    public int Damage;

    public float KnockTime = 5;
    private float KnockCounter = 0;

    private float Angle;
    private float Direc;    

    private bool isRetreating = false; // 是否正在后退
    private bool isCircling = false; // 是否正在绕圈

    private Vector2 Direction;

    private Transform PlayerTransform;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = ArkMovement.Instance.transform;
    }
    // Update is called once per frame
    void Update()
    {
        KnockCounter-=Time.deltaTime;
        Direction = PlayerTransform.position - transform.position;
        //朝向玩家
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        // 应用旋转
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
            // 触发后退逻辑
            isRetreating = true;
            isCircling = false;
            Direc = Random.value;
            PlayerHealthController.instance.TakeDamage(Damage);
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
        // 后退到指定距离
        transform.position += -(Vector3)Direction * Speed * Time.deltaTime;

        // 检查是否达到后退距离
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerTransform.position);
        if (distanceToPlayer >= Radius + Random.Range(0,1))
        {
            isRetreating = false;
            isCircling = true;           
            // 初始化圆周运动的角度
            Vector2 direction = transform.position - PlayerTransform.position;
            Angle = Mathf.Atan2(direction.y, direction.x);
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
        float x = PlayerTransform.position.x + (Radius + Random.Range(0, 1)) * Mathf.Cos(Angle);
        float y = PlayerTransform.position.y + (Radius + Random.Range(0, 1)) * Mathf.Sin(Angle);

        // 更新敌人位置        
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), 0.5f);
    }
}
