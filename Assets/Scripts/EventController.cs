using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public GameObject Light;
    public bool IsOpenMap = false;
    public GameObject boss;
    public CameraFollow Camera;
    public GameObject PlayerOnMap;
    public GameObject MapLight;
    public GameObject BossHealthUi;
    // Start is called before the first frame update
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
    }

    public void BossFight()
    {
        PlayerHealthController.instance.transform.position = new Vector3(1600, -8, 0);
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
