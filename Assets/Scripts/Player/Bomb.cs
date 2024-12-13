using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject artillery;
    public float Speed;
    public float LifeTime;

    private float LifeTimeCounter;
    private Vector3 Direction;
    void Start()
    {
        LifeTimeCounter = LifeTime;
        Direction = artillery.transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;
        LifeTimeCounter -= Time.deltaTime;
        if (LifeTimeCounter < 0 )
        {
            Destroy(gameObject);
        }    
    }
}
