using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController Instance;   
    public List<GameObject> jobs ;//6个位置    
    public List<GameObject> animals ;//6个动物
    private string selectedJob = "";//存储当前选择的职位
    private Dictionary<string,string> animalJobMapping = new Dictionary<string,string>();//记录每个动物的职位

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


    public void ShowChew(string Number)
    {
        // 用以标记职位的Number
        selectedJob = Number; 
        ChewPanel.SetActive(true);
    }

    public void OnDuty(string Number)
    {
        // 如果没有职位被选中
        if (string.IsNullOrEmpty(selectedJob))
        {
            // todo打印“请选择职位”
        }
        // 如果有职位被选中且该人员尚未被分配职位
        else if (!animalJobMapping.ContainsKey(Number))
        {
            // 将人员分配给选定的职位
            animalJobMapping[Number] = selectedJob;          
            // 重置选定职位
            selectedJob = "";
        }
        // 如果该人员已经被分配了职位
        else if (animalJobMapping.ContainsKey(Number))
        {   
            // todo弹窗“该人物已有职位（），是否切换职位
            // 将人员分配给选定的职位
            animalJobMapping[Number] = selectedJob;
            // 重置选定职位
            selectedJob = "";
            
        }

    }

    //这个方法用于返回某个职位的人员string
    public string GetPersonByJob(string job)
    {
        foreach(var position in animalJobMapping)
        {
            if(position.Value == job)
            {
                return position.Key; //返回对应人员
            }
        } 
        return null; // 未找到
    }
}

