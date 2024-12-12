using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//玩家运动脚本
//采用了AD控制方向，WS控制移动的运动原理。
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public Rigidbody2D rb;
    public float Speed;
    public float R_Speed;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Input.GetAxis("Vertical") * transform.up * Speed;
        rb.angularVelocity = Input.GetAxis("Horizontal") * R_Speed * -1;
    }
}
