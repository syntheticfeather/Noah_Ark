using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public EnemyHealthController Bosshealth;
    public Slider health;
    // Start is called before the first frame update
    void Start()
    {
        health.maxValue = Bosshealth.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health.value = Bosshealth.CurHealth;
    }
}
