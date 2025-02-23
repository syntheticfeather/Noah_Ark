using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeManager : MonoBehaviour
{
    private float maxHealth; // 最大生命值
    private float maxShield; //最大盾条
    private float currentHealth; // 当前生命值
    private float currentShield; // 当前盾条
    public Text text_1;//两个船的属性文本
    public Text text_2;
    // Start is called before the first frame update
    void Start()
    {
        //初始化血条盾条的值
        maxHealth = PlayerHealthController.instance.Health;
        maxShield = PlayerHealthController.instance.Shield;
        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    // Update is called once per frame
    void Update()
    {
        //实时反馈血条盾条变化
        currentHealth = PlayerHealthController.instance.Health;
        currentShield = PlayerHealthController.instance.Shield;
        text_1.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        text_2.text = currentShield.ToString() + "/" + maxShield.ToString();
    }
}
