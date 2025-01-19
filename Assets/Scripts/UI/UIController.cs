using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    public int Position;

    public GameObject ChewManagerPanel; // �װ�������
    public GameObject ChewPanel; // ������ʾ����
    // �涨 �캽ԱΪ0�����ִ����ң����ϵ�������Ϊ1��2��3��4������Ϊ5.
    void Start()
    {
        Instance = this;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ChewManagerPanel.activeSelf == false)
            {
                ChewManagerPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                ChewManagerPanel.SetActive(false);
                ChewPanel.SetActive(false);
                Time.timeScale = 1f;                
            }
        }
    }


    public void ShowChew(int Number)
    {
        // ���Ա��ְλ��Number
        Position = Number;
        Debug.Log(Position);
        ChewPanel.SetActive(true);
    }

    public void OnDuty(int Number)
    {
        //���ڱ�Ƕ����Number

    }
}
