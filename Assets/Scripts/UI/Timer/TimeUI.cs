using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public static TimeUI instance;
    public TMP_Text timerText; // �󶨵� UI Text ���
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
            // �Ӵ浵�м���ʱ��
            GameData gameData = saveLoadManager.GetTimeData();
            if (gameData != null)
            {
                WholeTime = gameData.wholeTime;
                
            }
        }
        else
        {
            // �޴浵ʱ����ʼ��ʱ��Ϊ30����
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