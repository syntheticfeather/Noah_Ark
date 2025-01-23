using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public static EnemyHealthController Instance;
    private int CurHeatlh;
    public int MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        CurHeatlh = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int Damage)
    {
        CurHeatlh -= Damage;
        if (CurHeatlh <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeHeal(int Heal)
    {
        CurHeatlh += Heal;
        if (CurHeatlh >= MaxHealth)
        {
            CurHeatlh = MaxHealth;
        }
        Debug.Log("getheal");
    }
}
