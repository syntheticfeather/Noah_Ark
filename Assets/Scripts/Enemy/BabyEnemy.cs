using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyEnemy : MonoBehaviour
{
    // 正常进展逻辑，外加护卫母体
    public int Damage;
    public float Speed;
    private float Angle;

    public float KnockTime = 5;
    private float KnockCounter = 0;


    public float staggerDuration = 2f; // 僵直持续时间
    private bool isStaggered = false; // 是否处于僵直状态

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
        //朝向玩家
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        // 应用旋转
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
        // 进入僵直状态
        isStaggered = true;
        Debug.Log("Monster is staggered!");

        rb.velocity = Vector2.zero;

        // 等待僵直时间结束
        yield return new WaitForSeconds(staggerDuration);

        // 结束僵直状态
        isStaggered = false;
        Debug.Log("Monster is no longer staggered!");

    }
}