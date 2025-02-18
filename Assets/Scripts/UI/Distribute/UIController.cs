using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController Instance;   
    public List<GameObject> jobs ;//6个位置    
    public List<GameObject> animals ;//9个可能动物
    private string selectedJob = "";//存储当前选择的职位
    private Dictionary<string,string> animalJobMapping = new Dictionary<string,string>();//记录每个动物的职位

    public GameObject ChewManagerPanel; // 甲板分配界面
    public GameObject ChewPanel; // 动物显示界面
    // 规定 领航员为0，炮手从左到右，从上到下依次为1、2、3、4，舵手为5.
    void Start()
    {
        Instance = this;
        UpdateResourceUI();
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
        AnimalPanel.instance.InitPanel();
        Debug.Log("ShowChew:" + Number);
    }



    public void OnDuty(string Number)// number是在动物列表中的位置
    {
        // select是炮中的位置
        Debug.Log("OnDuty:" + Number);
        // 如果没有职位被选中
        if (string.IsNullOrEmpty(selectedJob))
        {
            // todo打印“请选择职位”
            Debug.Log("没有string");
        }
        //// 如果有职位被选中且该人员尚未被分配职位
        //else if (!animalJobMapping.ContainsKey(Number))
        //{
        //    // 将人员分配给选定的职位
        //    animalJobMapping[Number] = selectedJob;          
        //    // 重置选定职位
        //    selectedJob = "";
        //}
        //// 如果该人员已经被分配了职位
        //else if (animalJobMapping.ContainsKey(Number))
        //{   
        //    // todo弹窗“该人物已有职位（），是否切换职位
        //    // 将人员分配给选定的职位
        //    animalJobMapping[Number] = selectedJob;
        //    // 重置选定职位
        //    selectedJob = "";            
        //}
        WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<PlayerWeapon>().enabled = false;
        WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<TurretAI>().enabled = false;
        if (ChewManager.Instance.allCrews[int.Parse(Number)].name == "Noah")
        {
            //将playerweapon脚本打开
            Debug.Log("Noah启用鼠标跟踪脚本");
            WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<PlayerWeapon>().enabled = true;
            WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<PlayerWeapon>().Animal = ChewManager.Instance.allCrews[int.Parse(Number)].gameObject;
        }
        else
        {
            //打开AI的weapon脚本
            Debug.Log("AI启用自动跟踪脚本");
            WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<TurretAI>().enabled = true;
            WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<TurretAI>().Animal = ChewManager.Instance.allCrews[int.Parse(Number)].gameObject;
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
    public TMP_Text ResourceUI;
    public void UpdateResourceUI()
    {
        ResourceUI.text = "Wood: " + ResourceManager.instance.Resource[0] + "\n"
            + "Stone: " + ResourceManager.instance.Resource[1] + "\n"
            + "Food: " + ResourceManager.instance.Resource[2] + "\n"
            + "Crystal: " + ResourceManager.instance.Resource[3];
    }
}

