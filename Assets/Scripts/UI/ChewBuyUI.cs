using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChewBuyUI : MonoBehaviour
{
    public static ChewBuyUI instance;
    public int CurChewindex;
    public TMP_Text Chewdata;
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

    }
}
