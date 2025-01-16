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
    private float DifferAngle;
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
        Attack();
        FollowMouse();
    }    

    //攻击
    public void Attack()
    {
        if (CoolDownCounter < 0 && Input.GetAxisRaw("Fire1") == 1)
        {
            //根据方向，实例化炸药
            GameObject BombToSpawn = Instantiate(Bomb, this.transform.position, Quaternion.identity);
            BombToSpawn.SetActive(true);           

            //进入冷却状态
            CoolDownCounter = CoolDown;
        }
    }

    //跟随鼠标位置
    public void FollowMouse()
    {

        //拿到鼠标位置
        MousePos = Input.mousePosition;

        //转换为世界坐标系，然后向量相减,得方向。
        Direction = Camera.main.ScreenToWorldPoint(MousePos) - this.transform.position;

        // 计算旋转角度，要减一个90度，untiy左手系。
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;

        // 创建旋转四元数并应用到物体
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //开始旋转
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotateSpeed * Time.deltaTime);        
    }
}