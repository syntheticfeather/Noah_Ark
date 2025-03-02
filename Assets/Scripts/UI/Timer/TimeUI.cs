using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public static TimeUI instance;
    public TMP_Text timerText; // 绑定的 UI Text 组件
    public float WholeTime;//
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        instance = this;
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager != null && saveLoadManager.HasSaveData())
        {
            // 从存档中加载时间
            GameData gameData = saveLoadManager.GetTimeData();
            if (gameData != null)
            {
                WholeTime = gameData.wholeTime;
                
            }
        }
        else
        {
            // 无存档时，初始化时间为30分钟
            WholeTime = 1800f;
           
        }
    }

    void Update()
    {
        WholeTime -= Time.deltaTime;            
        float mins = Mathf.FloorToInt(WholeTime / 60f);
        float secs = Mathf.FloorToInt(WholeTime % 60);
        timerText.text = mins + ":" + secs.ToString("00");        
    }
}