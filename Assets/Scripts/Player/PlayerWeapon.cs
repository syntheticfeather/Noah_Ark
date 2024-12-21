using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerWeapon : MonoBehaviour
{
    public GameObject Bomb;
    //规定0为向左攻击，右为向右攻击
    public int Direction;
    //攻击冷却时间。
    public float CoolDown;
    private float CoolDownCounter = 0;
    void Start()
    {
    }

    
    void Update()
    {
        //计时器如果小于0，表明冷却完毕，停止计时减少运算量
        if (CoolDownCounter >= 0)
        {
            CoolDownCounter -= Time.deltaTime;
        }
        //鼠标点击？
        //GetAxisRaw()会在鼠标左键点击时返回1
        if (CoolDownCounter<0 && Input.GetAxisRaw("Fire1") == 1)
        {
            GameObject BombToSpawn = Instantiate(Bomb, this.transform.position, Quaternion.identity);
           
            //科研
            //获取炸药上的组件
            BombToSpawn.GetComponent<Rigidbody2D>();

            BombToSpawn.SetActive(true);
            CoolDownCounter = CoolDown;
            
        }
        //按键开火？


    }
}
