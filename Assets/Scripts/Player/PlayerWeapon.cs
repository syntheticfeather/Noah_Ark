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
        //�������
        //GetAxisRaw()�������������ʱ����1
        if (Input.GetAxisRaw("Fire1") == 1)
        {
            Instantiate(Bomb,transform.position,Quaternion.identity);
        }
        //��������


    }
}
