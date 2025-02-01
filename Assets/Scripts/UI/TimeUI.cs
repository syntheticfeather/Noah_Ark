using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public static TimeUI instance;
    public TMP_Text timerText; // �󶨵� UI Text ���
    public float WholeTime;
       

    void Start()
    {
        instance = this;
        WholeTime = WholeTime * 60;
    }

    void Update()
    {
        WholeTime -= Time.deltaTime;            
        float mins = Mathf.FloorToInt(WholeTime / 60f);
        float secs = Mathf.FloorToInt(WholeTime % 60);
        timerText.text = mins + ":" + secs.ToString("00");        
    }
}