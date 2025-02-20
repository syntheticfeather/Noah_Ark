using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{    
    public static EventController instance;
    public GameObject Light;
    public bool IsOpenMap = false;
    public GameObject boss;
    public CameraFollow Camera;
    public GameObject PlayerOnMap;
    public GameObject MapLight;
    public GameObject BossHealthUi;
    public GameObject Map;
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
        if (Vector3.Distance(PlayerHealthController.instance.transform.position,Vector3.zero) <= 40)
        {
            BossFight();           
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!IsOpenMap)
            {
                OpenMap();
            }
            else
            {
                CloseMap();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChewManager.Instance.AllBack();
        }
        if (TimeUI.instance.WholeTime <= 1200 || Input.GetKeyDown(KeyCode.U))// 索第一次圈 // 数值待定 // 测试
        {
            Map.transform.localScale = new Vector3(600, 600, 600);
            IsLandController.instance.UpdateIsLand();
        }
        if (TimeUI.instance.WholeTime <= 600)// 索第一次圈
        {
            Map.transform.localScale = new Vector3(400, 400, 400);
            IsLandController.instance.UpdateIsLand();
        }
    }

    public void BossFight()
    {
        PlayerHealthController.instance.transform.position = new Vector3(1600, -8, 0);
        Light.transform.position = new Vector3(1600, -8, 0);
        Light.SetActive(true);
        CameraFollow.instance.BossFight = true;
        boss.SetActive(true);
        BossHealthUi.SetActive(true);
    }
    public void OpenMap()
    {
        Time.timeScale = 0f;
        Camera.Map = true;
        IsOpenMap = true;
        PlayerOnMap.SetActive(true);
        MapLight.SetActive(true);
}
    public void CloseMap()
    {
        Time.timeScale = 1f;
        Camera.Map = false;
        IsOpenMap = false;
        PlayerOnMap.SetActive(false);
        MapLight.SetActive(false);
        Camera.transform.position = PlayerOnMap.transform.position;
    }
}
