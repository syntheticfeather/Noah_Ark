using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    
    public static PlayerHealthController instance;
    public float Health;
    public float MaxHealth;
    public float Shield;

    // Start is called before the first frame update
    public ExternalUpgrade externalUpgrade;

    void Start()
    {
        // 确保 ExternalUpgrade 脚本已赋值
        if (externalUpgrade == null)
        {
            return;
        }

        // 检查 Shield 技能是否解锁
        bool isShieldUnlocked = externalUpgrade.IsSkillUnlocked("Shield");

        // 根据解锁情况执行调试逻辑
        if (isShieldUnlocked)
        {
            Shield = 30f;
        }
        else
        {
            Shield = 10f;
        }
    }
    void Awake()
    {

        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int Damage)
    {
        if (Shield > 0)
        {
            Shield -= Damage;
            if (Shield < 0){ Shield = 0; }
        }
        else
        {
            Health -= Damage;
        }
        CameraShake.instance.Shake();
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Repair(float percent)
    {
        float healing =percent * MaxHealth;
        if (Health == MaxHealth)
        {
            Debug.Log("生命值已满");
        }
        if (Health > 0 && Health != MaxHealth)
        {
            if ((healing + Health) >= MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += healing;
            }
        }
    }

}
