using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject artillery;
    public float Speed;
    public float Decration;
    public float ChangeRate;

    public float LifeTime;
    private float LifeTimeCounter;

    private Vector3 Direction;

    void Start()
    {
        LifeTimeCounter = LifeTime;
        Direction = artillery.transform.up;
    }

    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;
        if (Speed > 0)
        {
            Speed -= Time.deltaTime * Decration;
        }
        if (Speed <= 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, ChangeRate);
            if (transform.localScale == Vector3.zero)
            {
                Destroy(gameObject);
            }
        }
    }  
}
