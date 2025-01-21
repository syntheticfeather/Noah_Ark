using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsLandController : MonoBehaviour
{

    public List<GameObject> IsLand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLand[0].GetComponent<Land>().CanLand && Input.GetKeyDown(KeyCode.E))
        {
            if (CameraFollow.instance.followArk)
            {
                ToLand();
            }else
            {
                ToShip();
            }           
        }
    }
    //µÇÂ½,ÇÐ»»¾µÍ·ÒÆ¶¯·½Ê½¡£
    public void ToLand() 
    {
        CameraFollow.instance.followArk = false;
        //Í£¿¿¶¯»­£¬ÔÝ¶¨
        ArkMovement.Instance.rb.velocity = Vector3.zero;
    }
    public void ToShip()
    {
        CameraFollow.instance.followArk = true;
    }
}
