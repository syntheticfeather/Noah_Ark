using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public static Navigator instance;

    public bool IsUsing;  
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsUsing)
        {
            //显示UI
        }
    }
}
