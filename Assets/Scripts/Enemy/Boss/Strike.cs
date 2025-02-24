using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public int ATK;
    public bool OneATK = false;
    void Start()
    {        
        Destroy(gameObject, 2);
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
