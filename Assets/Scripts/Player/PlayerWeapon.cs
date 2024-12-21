using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class PlayerWeapon : MonoBehaviour
{
    public GameObject Bomb;
    public GameObject Ark;
    private Vector3 MousePos;
    private Vector3 Direction;

    public float RotateSpeed;
    //攻击冷却时间。
    public float CoolDown;
    private float CoolDownCounter = 0;
    void Start()
    {
    }


    void Update()
    {
        //-----------------------------------------------------------------------------------------------//
        //计时器如果小于0，表明冷却完毕，停止计时减少运算量
        if (CoolDownCounter >= 0)
        {
            CoolDownCounter -= Time.deltaTime;
        }
        //鼠标点击？
        //GetAxisRaw()会在鼠标左键点击时返回1
        if (CoolDownCounter < 0 && Input.GetAxisRaw("Fire1") == 1)
        {
            //根据方向，实例化炸药
            GameObject BombToSpawn = Instantiate(Bomb, this.transform.position, Quaternion.identity);
            BombToSpawn.SetActive(true);

            //科研
            //获取炸药上的组件
            BombToSpawn.GetComponent<Rigidbody2D>();


            //进入冷却状态
            CoolDownCounter = CoolDown;

        }
        //----------//
        //跟随鼠标位置
        //-----------------------------------------------------------------------------------------------//
        //拿到鼠标位置
        MousePos = Input.mousePosition;        

        //转换为世界坐标系，然后向量相减。
        Direction = Camera.main.ScreenToWorldPoint(MousePos) - this.transform.position;       

        // 计算旋转角度，要减一个90度，别管为什么。
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;        
        
        // 创建旋转四元数并应用到物体
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //开始旋转
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation,RotateSpeed * Time.deltaTime);
        //这个是真正的360的角度，别问，试出来的。
        //Debug.Log(transform.rotation.eulerAngles.z);
        //-----------------------------------------------------------------------------------------------//

        //----------//
        //限制在Ark的左右各90度范围内。
        //还未实现
        //-----------------------------------------------------------------------------------------------//
        Debug.Log(transform.rotation.eulerAngles.z);

        if (transform.rotation.eulerAngles.z >= 90 && transform.rotation.eulerAngles.z <= 180)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));            
        }
        if (transform.rotation.eulerAngles.z <= 270 &&  transform.rotation.eulerAngles.z >= 180)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
