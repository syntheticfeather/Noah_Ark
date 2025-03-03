using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArkStatusManager : MonoBehaviour
{
    public Animator animator; //失败界面动画
    public GameObject deathPanel; // 失败界面
    public Image healthFillImage; // 血条的填充部分
    public Image shieldFillImage; // 盾条的填充部分
    public Image healthFillImage1; // 血条的渐变填充部分
    public Image shieldFillImage1; // 盾条的渐变填充部分
    private float maxHealth; // 最大生命值
    private float maxShield; //最大盾条
    private float currentHealth; // 当前生命值
    private float currentShield; // 当前盾条
    private float currentHealth1; // 渐变生命值
    private float currentShield1; // 渐变盾条
    private float _LerpSpeed = 3; // 插值渐变
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = PlayerHealthController.instance.Health;
        currentHealth = maxHealth;
        currentHealth1 = maxHealth;
        if (ExternalUpgrade.Instance.skills[0].level==0)
        {
            shieldFillImage.enabled = false;
            shieldFillImage1.enabled = false;                        
            return;
        }
        //初始化血条盾条
        maxShield = PlayerHealthController.instance.Shield;
        currentShield = maxShield;
        currentShield1 = maxShield;

    }

    // Update is called once per frame
    void Update()
    {
        //实时反馈血条盾条变化
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
        // 更新填充图像的尺寸以反映当前的生命值和盾
        healthFillImage.fillAmount = currentHealth / maxHealth;
        shieldFillImage.fillAmount = currentShield / maxShield;
        healthFillImage1.fillAmount = Mathf.Lerp(a: healthFillImage1.fillAmount, b: currentHealth / maxHealth, t: _LerpSpeed * Time.deltaTime);
        shieldFillImage1.fillAmount = Mathf.Lerp(a: shieldFillImage1.fillAmount, b: currentShield / maxShield, t: _LerpSpeed * Time.deltaTime);
    }

    private void OnDeath()
    {
        // 游戏结束逻辑，加载失败场景
        deathPanel.SetActive(true);
        PausePanelManager.instance.GameIsDead = true;
        animator.SetBool("IsDead", true);
    }
}

