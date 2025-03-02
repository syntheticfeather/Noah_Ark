using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    
    public static PlayerHealthController instance;
    public float Health;
    public float MaxHealth;
    public float Shield;
    public float Shield2;
    public float Counter;
    // Start is called before the first frame update
    public ExternalUpgrade externalUpgrade;

    void Start()
    {
        Health = MaxHealth;
        int ShieldLevel = ExternalUpgrade.Instance.skills[0].level;
        Debug.Log(ShieldLevel);
        switch (ShieldLevel)
        {
            case 0:
                Shield = 0f;
                Debug.Log("No upgrade selected");
                break;
            case 1:
                Shield = 10f;
                Debug.Log("Upgrade 1 selected");
                break;
            case 2:
                Shield = 20f;
                Debug.Log("Upgrade 2 selected");
                break;
            case 3:
                Shield = 50f;
                Debug.Log("Upgrade 3 selected");
                break;
            default:
                break;
        }
        Shield2 = Shield;
    }
    void Awake()
    {
        instance = this;        
    }

    // Update is called once per frame
    void Update()
    {
        Counter -= Time.deltaTime;
        if (Counter <= 0)
        {
            Shield = Shield2;
            Counter = 45;
        }
    }
    
    public void TakeDamage(int Damage)
    {
        if (ExternalUpgrade.Instance.skills[0].level != 0 && Shield > 0)
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

    public void Repair(float percent)// .2f
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
