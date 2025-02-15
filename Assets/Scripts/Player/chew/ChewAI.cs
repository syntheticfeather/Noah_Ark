using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ChewAI : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float harvestTime = 2f;
    public Transform shipDepositPoint;
    private Vector3 Target;
    
    private Resource currentResource;
    private bool isCarryingWood;
    private bool isonark = true;


    void Start()
    {
    }

    public void AssignTask(Resource resource)
    {
        isonark = false;
        currentResource = resource;
        Target = currentResource.transform.position;
    }

    void Update()
    {
        if (currentResource == null) return;

        // 检测到达目标
        if (isCarryingWood)
        {               
            { 
                DeliverToShip();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, Target) > 0.5f)
            {
                // 移动到目标，不用导航            
                transform.position = Vector3.MoveTowards(transform.position, Target, moveSpeed * Time.deltaTime);

            }
            else
            {
                StartCoroutine(HarvestTree());
            }
        }


    }

    IEnumerator HarvestTree()
    {
        // 播放砍伐动画
        //GetComponent<Animator>().SetTrigger("Chop");

        yield return new WaitForSeconds(harvestTime);

        // 获取资源
        Target = shipDepositPoint.position;
        currentResource.gameObject.SetActive(false);
        currentResource.selectionEffect.SetActive(false);
        isCarryingWood = true;   
    }

    void DeliverToShip()
    {
        if (isonark)
        {
            // 将木材存入船只
            ResourceManager.instance.AddResource(currentResource.woodAmount, 0);
            isCarryingWood = false;
            currentResource = null;
            transform.position = shipDepositPoint.position;
            // 回到闲置状态
            ChewManager.Instance.ReturnIdleCrew(this);

        }
        else
        {
            //回到船只
            transform.position = Vector3.MoveTowards(transform.position, shipDepositPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, shipDepositPoint.position) < 0.5f)
            {
                isonark = true;
            }
        }
    }
}