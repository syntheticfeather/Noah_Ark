using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimalPanel : MonoBehaviour
{
    public static AnimalPanel instance;
    // ��ť�б�
    public List<GameObject> AnimalButtonList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPanel()
    {
        int i = 0;
        foreach (var item in ChewController.instance.ChewList)
        {
            if (item != null)
            {
                Debug.Log("��ʼ���ͼƬ");
                AnimalButtonList[i].GetComponent<ChewButton>().UpdateText(item.GetComponent<Chew>().sprite);
            }//������ֵ,�ݶ�
            i++;
            if (i >= ChewController.instance.MaxChew)
            {
                break;
            }
        }
    }
}
