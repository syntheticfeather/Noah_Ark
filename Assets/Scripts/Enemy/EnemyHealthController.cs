using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public static EnemyHealthController Instance;
    public int CurHealth;
    public int MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        CurHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int Damage)
    {
        
        CurHealth -= Damage;
        if (CurHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void TakeHeal(int Heal)
    {
        CurHealth += Heal;
        if (CurHealth >= MaxHealth)
        {
            CurHealth = MaxHealth;
        }
        Debug.Log("getheal");
    }
}
