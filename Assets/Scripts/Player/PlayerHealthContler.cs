using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthContler : MonoBehaviour
{
    public int Health;


    public static PlayerHealthContler instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        CameraShake.instance.Shake();
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
