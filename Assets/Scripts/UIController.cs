using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public GameObject ChewManagerPanel;
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
                Time.timeScale = 1f;                
            }
        }
    }


    public void ShowChew()
    {

    }
}
