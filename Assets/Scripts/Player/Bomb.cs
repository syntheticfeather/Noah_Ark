using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float Speed;
    public float LifeTime;
    private float LifeTimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        LifeTimeCounter = LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += PlayerMovement.Instance.transform.right * Speed * Time.deltaTime;
        LifeTimeCounter -= Time.deltaTime;
        if (LifeTimeCounter < 0 )
        {
            Destroy(gameObject);
        }    
    }
}
