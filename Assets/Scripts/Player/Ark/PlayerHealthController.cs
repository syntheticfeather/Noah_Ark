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
        // ȷ�� ExternalUpgrade �ű��Ѹ�ֵ
        if (externalUpgrade == null)
        {
            return;
        }

        // ��� Shield �����Ƿ����
        bool isShieldUnlocked = externalUpgrade.IsSkillUnlocked("Shield");

        // ���ݽ������ִ�е����߼�
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
            Debug.Log("����ֵ����");
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
