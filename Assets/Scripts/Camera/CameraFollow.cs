using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // 平滑速度
    
    public bool followArk;
    public bool BossFight;
    public bool Map;
    public float moveSpeed = 5f; // 移动速度
    public float smoothTime = 0.1f; // 平滑时间

    public Vector3 TargetPosition = Vector3.zero;

    public static CameraFollow instance;
    public GameObject Target;
    public GameObject Light;
    void Start()
    {
        transform.position = Target.transform.position;
        instance = this;
        BossFight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BossFight)
        {
            if (followArk)
            {
                FollowArk();
            }
            if (!followArk && !Map)
            {
                FreeMovement();
            }
            if (Map)
            {
                MapCamera();
            }           
        }
        else
        {            
            Stay();
        }

    }
    public void FollowArk()
    {
        //Debug.Log(transform.position);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, Target.transform.position + new Vector3(0, 0, -10), smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.GetComponent<Camera>().orthographicSize = 20;
        Light.transform.position = Target.transform.position;
    }

    void FreeMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 
        TargetPosition += new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, TargetPosition, smoothTime);
        transform.GetComponent<Camera>().orthographicSize = 20;
        Light.transform.position = transform.position + new Vector3(0, 0, 10);
    }

    public void MapCamera()
    {
        transform.position = Vector3.zero + new Vector3(0,0, -10);
        transform.GetComponent<Camera>().orthographicSize = 500;
    }

    public void Stay()
    {
        transform.GetComponent<Camera>().orthographicSize = 20;
        transform.position = new Vector3(1600,-8,-10);
        SpawnTimer.instance.UpdateMaxSlider();
    }
}