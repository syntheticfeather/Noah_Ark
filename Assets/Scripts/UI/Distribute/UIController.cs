using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

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
        // 该位置的按钮显示动物图像




        GameObject targetAnimal = ChewManager.Instance.allCrews[int.Parse(Number)].gameObject;

        // 定义一个Predicate来检查GameObject是否与targetAnimal相同
        Predicate<GameObject> isTargetAnimal = (animal) => animal == targetAnimal;

        if (WeaponManager.Instance.AnimalList.Exists(isTargetAnimal))
        {
            //关闭原有位置的脚本
            int index = WeaponManager.Instance.AnimalList.FindIndex(isTargetAnimal);
            WeaponManager.Instance.AnimalList[index] = null;
            WeaponManager.Instance.WeaponList[index].GetComponent<PlayerWeapon>().enabled = false;
            WeaponManager.Instance.WeaponList[index].GetComponent<TurretAI>().enabled = false;
            // 位置图片删除
            jobs[index].GetComponent<ChewButton>().UpdateText(null);
        }        
        //左侧职位图片更新
        Sprite i = ChewManager.Instance.allCrews[int.Parse(Number)].GetComponent<Chew>().Sprite_Pic;
        jobs[int.Parse(selectedJob)].GetComponent<ChewButton>().UpdateText(i);  
        //设置武器脚本
        //设置脚本
        if (ChewManager.Instance.allCrews[int.Parse(Number)].name == "Noah")
        {
            //将playerweapon脚本打开
            Debug.Log("Noah启用鼠标跟踪脚本");
            WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<PlayerWeapon>().enabled = true;
            WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<PlayerWeapon>().Animal =
                ChewManager.Instance.allCrews[int.Parse(Number)].gameObject;
            WeaponManager.Instance.AnimalList[int.Parse(selectedJob)] = ChewManager.Instance.allCrews[int.Parse(Number)].gameObject;
        }
        else
        {
            //打开AI的weapon脚本
            Debug.Log("AI启用自动跟踪脚本");
            WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<TurretAI>().enabled = true;
            WeaponManager.Instance.WeaponList[int.Parse(selectedJob)].GetComponent<TurretAI>().Animal = 
                ChewManager.Instance.allCrews[int.Parse(Number)].gameObject;
            WeaponManager.Instance.AnimalList[int.Parse(selectedJob)] = ChewManager.Instance.allCrews[int.Parse(Number)].gameObject;
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

