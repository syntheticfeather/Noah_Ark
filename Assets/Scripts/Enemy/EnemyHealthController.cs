using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public static EnemyHealthController Instance;
    public int Heatlh;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int Damage)
    {
        Heatlh -= Damage;
        if (Heatlh <= 0)
        {
            Destroy(gameObject);
        }
    }
}
