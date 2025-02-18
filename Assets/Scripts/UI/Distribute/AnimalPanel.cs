using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimalPanel : MonoBehaviour
{
    public static AnimalPanel instance;
    // 按钮列表
    public List<GameObject> AnimalButtonList = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitPanel()
    {
        int i = 0;
        foreach (var item in ChewManager.Instance.allCrews)
        {
            if (item != null && i < ChewManager.Instance.allCrews.Count)
            {
                Debug.Log("开始添加图片");
                Debug.Log( "第" + i);
                AnimalButtonList[i].SetActive(true);
                AnimalButtonList[i].GetComponent<ChewButton>().UpdateText(item.GetComponent<Chew>().sprite);
            }//其他数值,暂定
            if (i >= ChewManager.Instance.allCrews.Count || item == null)
            {
                Debug.Log("隐藏图片");

                AnimalButtonList[i].SetActive(false);
            }
            i++;
        }
    }
}
