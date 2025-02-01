using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealthController : MonoBehaviour
{
    public static EnemyHealthController Instance;
    public int CurHealth;
    public int MaxHealth;
    public SpriteRenderer renderer2D; // 物体的渲染组件
    private Color originalColor; // 记录原始颜色
    public float flashDuration = 0.1f; // 变红持续时间
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        CurHealth = MaxHealth;        
        originalColor = renderer2D.color; // 保存原始颜色
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int Damage)
    {
        StartCoroutine(FlashRed());
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
    IEnumerator FlashRed()
    {
        // 将颜色设为红色
        renderer2D.material.color = Color.red;

        // 等待一段时间后恢复原色
        yield return new WaitForSeconds(flashDuration);

        renderer2D.material.color = originalColor;
    }

}
