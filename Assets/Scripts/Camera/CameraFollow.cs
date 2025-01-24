using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // 平滑速度
    
    public bool followArk;
    public float moveSpeed = 5f; // 移动速度
    public float smoothTime = 0.1f; // 平滑时间

    public Vector3 TargetPosition = Vector3.zero;

    public static CameraFollow instance;
    public GameObject Target;   
    void Start()
    {
        transform.position = Target.transform.position;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (followArk)
        {
            FollowArk();
        }
        if (!followArk)
        {
            FreeMovement();
        }
    }
    public void FollowArk()
    {
        //Debug.Log(transform.position);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, Target.transform.position + new Vector3(0, 0, -10), smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;             
    }

    void FreeMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 
        TargetPosition += new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, TargetPosition, smoothTime);
    }

}