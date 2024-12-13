using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerWeapon : MonoBehaviour
{
    public GameObject Bomb;
    //�涨0Ϊ���󹥻�����Ϊ���ҹ���
    public int Direction;
    //������ȴʱ�䡣
    public float CoolDown;
    private float CoolDownCounter = 0;
    void Start()
    {
    }

    
    void Update()
    {
        //��ʱ�����С��0��������ȴ��ϣ�ֹͣ��ʱ����������
        if (CoolDownCounter >= 0)
        {
            CoolDownCounter -= Time.deltaTime;
        }
        //�������
        //GetAxisRaw()�������������ʱ����1
        if (CoolDownCounter<0 && Input.GetAxisRaw("Fire1") == 1)
        {
            Instantiate(Bomb,this.transform.position,Quaternion.identity);
            CoolDownCounter = CoolDown;
        }
        //��������


    }
}
