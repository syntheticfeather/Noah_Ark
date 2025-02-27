using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChewBuyUI : MonoBehaviour
{
    public static ChewBuyUI instance;
    public int CurChewindex;
    public GameObject BuyUI;
    public TMP_Text Chewdata;
    public TMP_Text Name;
    public TMP_Text DebugText;
    public Image ChewImage;
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

    public void ShowData()
    {
        Name.text = "" + ChewManager.Instance.CrewsToBuy[CurChewindex].name;
        Chew chew = ChewManager.Instance.CrewsToBuy[CurChewindex].GetComponent<Chew>();
        ChewImage.sprite = chew.sprite;
        Chewdata.text = "Cost: " + chew.Stats.Cost + " food" + "\n"
            + "Damage: " + chew.Stats.Damage + "\n"
            + "";

            //"\n" + "Cost:" + ChewManager.Instance.CrewsToBuy[CurChewindex].GetComponent<Chew>().cost.ToString();
    }
}
