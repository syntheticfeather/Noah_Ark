using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeManager : MonoBehaviour
{
    private float maxHealth; // �������ֵ
    private float maxShield; //������
    private float currentHealth; // ��ǰ����ֵ
    private float currentShield; // ��ǰ����
    public Text text_1;//�������������ı�
    public Text text_2;
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ��Ѫ��������ֵ
        maxHealth = PlayerHealthController.instance.Health;
        maxShield = PlayerHealthController.instance.Shield;
        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    // Update is called once per frame
    void Update()
    {
        //ʵʱ����Ѫ�������仯
        currentHealth = PlayerHealthController.instance.Health;
        currentShield = PlayerHealthController.instance.Shield;
        text_1.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        text_2.text = currentShield.ToString() + "/" + maxShield.ToString();
    }
}
