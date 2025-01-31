using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Navigator : MonoBehaviour
{
    public float Rate;
    public static Navigator instance;
    public GameObject ArkLight;
    public bool IsUsing;
    public bool Flag = false;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsUsing && !Flag)
        {
            //显示UI
            Flag = true;
            ArkLight.GetComponent<Light2D>().pointLightOuterRadius *= Rate;
        }
    }
}
