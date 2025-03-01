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
        int speedLevel = SkillLevelLoader.Instance.GetSkillLevel("Speed");

        switch (speedLevel)
        {
            case 0:
                Shield = 0f;
                break;
            case 1:
                Shield = 10f;
                break;
            case 2:
                Shield = 30f;
                break;
            case 3:
                Shield = 50f;
                break;
            default:
                break;
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
