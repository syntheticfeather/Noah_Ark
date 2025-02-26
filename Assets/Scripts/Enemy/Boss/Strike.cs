using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public int ATK;
    public bool OneATK = false;
    public Animator animator;
    void Start()
    {
        animator.Play("attack");
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("ÓÐÅö×²");
        if (collision.transform.tag == "Player" && OneATK == false)
        {
            OneATK = true;
            PlayerHealthController.instance.TakeDamage(ATK);
        }
    }
}
