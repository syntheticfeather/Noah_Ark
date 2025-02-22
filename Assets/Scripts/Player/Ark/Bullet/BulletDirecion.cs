using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirecion : MonoBehaviour
{
    public Vector3  direction;
    public float Speed;
    public float Decration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        transform.position += direction * Speed * Time.deltaTime;
        if (Speed > 0)
        {
            Speed -= Time.deltaTime * Decration;
        }
        if (Speed <= 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 10);
            if (transform.localScale == Vector3.zero)
            {
                Destroy(gameObject);
                Debug.Log("因速度为0而销毁");
            }
        }
    }
}
