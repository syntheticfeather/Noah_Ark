using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : MonoBehaviour
{
    public Animator animator; // Animator 组件
    public int ATK;
    public bool OneATK = false;
    void Start()
    {
        // 播放动画
        animator.Play("SweepAnim");
        Destroy(gameObject, 3);
    }
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("被扫开");
        if (collision.transform.tag == "Player" && OneATK == false)
        {
            OneATK = true;
            PlayerHealthController.instance.TakeDamage(ATK);
        }
    }
}