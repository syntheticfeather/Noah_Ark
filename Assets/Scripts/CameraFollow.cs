using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public GameObject Target;   
    public float smoothSpeed = 0.125f; // 平滑速度
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, Target.transform.position + new Vector3(0, 0, -10), smoothSpeed * Time.deltaTime);        
        transform.position = smoothedPosition;        
    }
}