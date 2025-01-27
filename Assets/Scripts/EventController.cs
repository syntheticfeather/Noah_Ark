using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public GameObject Light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerHealthController.instance.transform.position,Vector3.zero) <= 50)
        {
            BossFight();           
        }
    }

    public void BossFight()
    {
        PlayerHealthController.instance.transform.position = new Vector3(800, -8, 0);
        Light.SetActive(true);
        CameraFollow.instance.BossFight = true;
        Boss.Instance.gameObject.SetActive(true);
    }

}
