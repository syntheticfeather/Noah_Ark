using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 玩家运动脚本
// 采用了 AD 控制方向，WS 控制移动的运动原理。
public class ArkMovement : MonoBehaviour
{
    public static ArkMovement Instance;
    public Rigidbody2D rb;
    public float baseMoveSpeed = 5f;      // 基础移动速度
    public float baseMaxSpeed = 10f;     // 基础最大速度
    public float turnSpeed = 100f;       // 转向速度
    public float deceleration = 2f;      // 减速度

    private float moveSpeed;              // 实际移动速度
    private float maxSpeed;              // 实际最大速度

    // Dash 技能相关参数
    public float dashMultiplier ;    // Dash 速度倍率

    public float dashDuration ;      // Dash 持续时间
    private float dashTimer = 0f;        

    public float dashCooldown ;     // Dash 冷却时间
    private float cooldownTimer = 10f;    // 冷却计时器


    private bool isDashing = false;
    private bool isOnCooldown = true;   // 是否处于冷却状态


    // 引用 ExternalUpgrade 脚本
    public ExternalUpgrade externalUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        // 初始化速度
        InitializeSpeed();
        InitializeDash();
    }

    void InitializeSpeed()
    {
        // 默认使用基础速度
        moveSpeed = baseMoveSpeed;
        maxSpeed = baseMaxSpeed;

        // 获取 Speed 技能的等级
        int speedLevel = ExternalUpgrade.Instance.skills[1].level;
        Debug.Log("SpeedLevel: " + speedLevel);
        switch (speedLevel)
        {
            case 0:
                moveSpeed = baseMoveSpeed;
                maxSpeed = baseMaxSpeed;
                break;
            case 1:
                moveSpeed = baseMoveSpeed*1.4f;
                maxSpeed = baseMaxSpeed*1.4f;
                break;
            case 2:
                moveSpeed = baseMoveSpeed * 1.7f;
                maxSpeed = baseMaxSpeed * 1.7f;
                break;
            case 3:
                moveSpeed = baseMoveSpeed * 2f;
                maxSpeed = baseMaxSpeed * 2f;
                break;
            default:
                break;
        }
    }

    void InitializeDash()
    {
        int DashLevel = ExternalUpgrade.Instance.skills[3].level;
        Debug.Log("DashLevel: " + DashLevel);
        switch (DashLevel)
        {            
            case 0:
             dashMultiplier = 1f;    
             dashDuration = .5f;      
             dashCooldown = 16f; 
             break;
            case 1:
                dashMultiplier = 3f;
                dashDuration = 1f;
                dashCooldown = 12f;
                break;
            case 2:
                dashMultiplier = 5f;
                dashDuration = 1f;
                dashCooldown = 10f;
                break;
            case 3:
                dashMultiplier = 5f;
                dashDuration = 1.5f;
                dashCooldown = 5f;
                break;
            default:
                break;
        }
        dashTimer = dashDuration;
        cooldownTimer = 0;
    }

    void Update()
    {
        Movement();

        // 检查 Dash 技能是否解锁
        if (true)
        {
            // 如果按下 Z 键且未处于冷却状态，触发 Dash
            if (Input.GetKeyDown(KeyCode.Z) && !isDashing && !isOnCooldown)
            {
                StartDash();
                Debug.Log("Dash");
            }
        }

        // 如果正在 Dash，更新计时器
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                dashTimer = dashDuration;
                EndDash();
            }
        }

        // 如果处于冷却状态，更新冷却计时器
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
               isOnCooldown = false;
               cooldownTimer = dashCooldown;
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;

        // 增加速度
        moveSpeed *= dashMultiplier;
        maxSpeed *= dashMultiplier;
        
    }

    void EndDash()
    {
        isDashing = false;

        // 恢复速度
        moveSpeed = baseMoveSpeed;
        maxSpeed = baseMaxSpeed;

        // 如果 Speed 技能已解锁，重新应用 Speed 技能的加成
        if (externalUpgrade != null && externalUpgrade.IsSkillUnlocked("Speed"))
        {
            moveSpeed *= 1.5f;
            maxSpeed *= 1.5f;
        }

        // 启动冷却计时
        isOnCooldown = true;
        cooldownTimer = dashCooldown;
        
    }

    public void Movement()
    {
        if (CameraFollow.instance.followArk)
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