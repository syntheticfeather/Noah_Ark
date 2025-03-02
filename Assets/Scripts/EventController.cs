using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Map.GetComponent<SpriteRenderer>().color = color0;
    }
    public Color color0 = new Color(110f / 255f, 194f / 255f, 215f / 255f, 255f / 255f);
    public Color color1 = new Color(65f / 255f, 158f / 255f, 228f / 255f, 255f / 255f);
    public Color color2 = new Color(10f / 255f, 103f / 255f, 152f / 255f, 255f / 255f);
    public SpriteRenderer MapSpriteRenderer;
    // Update is called once per frame
    void Update()
    {    
        if (TimeUI.instance != null && PlayerHealthController.instance != null &&Vector3.Distance(PlayerHealthController.instance.transform.position, Vector3.zero) <= 40 || TimeUI.instance.WholeTime <= 0)
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

        if (TimeUI.instance.WholeTime <= 1200)// ����һ��Ȧ // ��ֵ���� // ����
        {
            Map.transform.localScale = new Vector3(600, 600, 600);
            Map.GetComponent<SpriteRenderer>().color = color1;
            IsLandController.instance.UpdateIsLand();
        }
        if (TimeUI.instance.WholeTime <= 600)// ����һ��Ȧ
        {
            Map.transform.localScale = new Vector3(400, 400, 400);
            Map.GetComponent<SpriteRenderer>().color = color2;
            IsLandController.instance.UpdateIsLand();
        }
             
    }
    public void BossFight()
    {
        AudioSequencePlayer.instance.stopPlayback();
        if (TimeUI.instance != null)
        {
            TimeUI.instance.gameObject.SetActive(false);
        }
        //Debug.Log("BossFight");
        PlayerHealthController.instance.transform.position = new Vector3(1600, -8, 0);
        // ��ӿ����ü��
        if (Light != null)
        {
            Light.transform.position = new Vector3(1600, -8, 0);
            Light.SetActive(true);
        }        
        if (CameraFollow.instance != null)
        {
            CameraFollow.instance.BossFight = true;
        }
        if (boss != null)
        boss.SetActive(true);
        if (BossHealthUi != null)
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
    public void End()
    {
        SFXManager.instance.PlaysfxPitch(SFXManager.instance.BossSound, 4);
        PlayerPrefs.SetFloat("CrystalCount", ResourceManager.instance.Resource[3]);
        PlayerPrefs.Save();
        var sss = GameObject.Find("ExternUpgrade");
        sss.GetComponent<ExternalUpgrade>().Upgrade();
        Invoke("LoadStartScene", 5f);
    }        
    public void LoadStartScene()
    {
        var canvas = GameObject.Find("MenuCanvas");
        canvas.GetComponent<ActivateChildren>().ActivateAllChildren();
        SceneManager.LoadScene("StartScene");
        SFXManager.instance.BossSound[0].Stop();
        AudioSequencePlayer.instance.audioSource.Stop();
    }
}
