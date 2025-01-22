using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArkStatusManager : MonoBehaviour
{
    public GameObject pausePanel; // ��ͣ����
    public Image healthFillImage; // Ѫ������䲿��
    public Image shieldFillImage; // ��������䲿��
    public Image healthFillImage1; // Ѫ���Ľ�����䲿��
    public Image shieldFillImage1; // �����Ľ�����䲿��
    private float maxHealth; // �������ֵ
    private float maxShield; //������
    private float currentHealth; // ��ǰ����ֵ
    private float currentShield; // ��ǰ����
    private float currentHealth1; // ��������ֵ
    private float currentShield1; // �������
    private float _LerpSpeed = 3; // ��ֵ����
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ��Ѫ������
        maxHealth = PlayerHealthController.instance.Health;
        maxShield = PlayerHealthController.instance.Shield;
        currentHealth = maxHealth;
        currentShield = maxShield;
        currentHealth1 = maxHealth;
        currentShield1 = maxShield;

    }

    // Update is called once per frame
    void Update()
    {
        //ʵʱ����Ѫ�������仯
        currentHealth = PlayerHealthController.instance.Health;
        currentShield = PlayerHealthController.instance.Shield;
        currentHealth1 = currentHealth;
        currentShield1 = currentShield;
        UpdateHealth();
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }
    private void UpdateHealth()
    {
        // �������ͼ��ĳߴ��Է�ӳ��ǰ������ֵ�Ͷ�
        healthFillImage.fillAmount = currentHealth / maxHealth;
        shieldFillImage.fillAmount = currentShield / maxShield;
        healthFillImage1.fillAmount = Mathf.Lerp(a: healthFillImage1.fillAmount, b: currentHealth / maxHealth, t: _LerpSpeed * Time.deltaTime);
        shieldFillImage1.fillAmount = Mathf.Lerp(a: shieldFillImage1.fillAmount, b: currentShield / maxShield, t: _LerpSpeed * Time.deltaTime);
    }

    private void OnDeath()
    {
        // ��Ϸ�����߼�������ʧ�ܳ���
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
}

