using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


//玩家运动脚本
//采用了AD控制方向，WS控制移动的运动原理。
public class ArkMovement : MonoBehaviour
{
    public static ArkMovement Instance;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public float moveSpeed = 5f;      // 移动速度
    public float turnSpeed = 100f;   // 转向速度
    public float maxSpeed = 10f;     // 最大速度
    public float deceleration = 2f;  // 减速度

    void Update()
    {
        Movement();
    }
    public void Movement()
    {
        if (CameraFollow.instance.followArk && Wheel.instance.IsUsing)
        {
            float moveInput = Input.GetAxis("Vertical");
            float turnInput = Input.GetAxis("Horizontal");
            MoveBoat(moveInput);
            TurnBoat(turnInput);
        }
    }


    void MoveBoat(float input)
    {
        if (input != 0)
        {
            // 计算移动方向（前方）
            Vector2 moveDirection = transform.up * input * moveSpeed * Time.deltaTime;
            // 施加力
            rb.AddForce(moveDirection, ForceMode2D.Impulse);
            // 限制最大速度
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            // 如果没有输入，逐渐减速
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, deceleration * Time.deltaTime);
        }
    }

    void TurnBoat(float input)
    {
        if (input != 0)
        {
            // 计算转向角度
            float turn = input * turnSpeed * Time.deltaTime;
            // 旋转船只
            transform.Rotate(0, 0, -turn); // 2D 中绕 Z 轴旋转
        }
    }
}
