using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float Speed;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Speed * transform.up * Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerHealthController.instance.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
