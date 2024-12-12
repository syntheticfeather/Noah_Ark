using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerWeapon : MonoBehaviour
{
    public GameObject Bomb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //鼠标点击？
        //GetAxisRaw()会在鼠标左键点击时返回1
        if (Input.GetAxisRaw("Fire1") == 1)
        {
            Instantiate(Bomb,transform.position,Quaternion.identity);
        }
        //按键开火？


    }
}
