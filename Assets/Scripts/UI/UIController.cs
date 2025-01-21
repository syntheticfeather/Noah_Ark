using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    public int Position;

    public GameObject ChewManagerPanel; // 甲板分配界面
    public GameObject ChewPanel; // 动物显示界面
    // 规定 领航员为0，炮手从左到右，从上到下依次为1、2、3、4，舵手为5.
    void Start()
    {
        Instance = this;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
        // 用以标记职位的Number
        Position = Number;
        Debug.Log(Position);
        ChewPanel.SetActive(true);
    }

    public void OnDuty(int Number)
    {
        //用于标记动物的Number

    }
}
