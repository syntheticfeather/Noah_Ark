using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ChewAI : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float harvestTime = 2f;
    public int harvestCapacity = 3;
    public int CurHarvestAmount;
    public Transform shipDepositPoint;
    private Vector3 Target;
    
    private Resource currentResource;
    private bool isCarryingWood;
    public bool OneStep = true;
    public bool FinishHarvest = false;
    public bool DirectToShip = false;
    public bool GetToWork;
    CircleCollider2D Collider;
    private void Awake()
    {
        Collider = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
    }

    public void AssignTask(Resource resource)
    {
        GetToWork = true;
        WeaponManager.Instance.UpdateStatus();
        transform.position = shipDepositPoint.position;
        gameObject.SetActive(true);
        Debug.Log("Assign Task");
        currentResource = resource;
        Target = currentResource.transform.position;
    }

    void Update()
    {
        if (DirectToShip)// G键强制回船
        {
            Debug.Log("Direct to ship");
            transform.position = Vector3.MoveTowards(transform.position, shipDepositPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, shipDepositPoint.position) < 3f)
            {
                GetToWork = false;
                WeaponManager.Instance.UpdateStatus();
                gameObject.SetActive(false);
            }
            return;
        }
        if (!isCarryingWood && (currentResource == null || currentResource.Amount <= 0))// 物资采集完提前回船。
        {
            transform.position = Vector3.MoveTowards(transform.position, shipDepositPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, shipDepositPoint.position) < 3f)
            {
                if (ChewManager.Instance.ResourceList.Count != 0)
                {
                    AssignTask(ChewManager.Instance.ResourceList[0]);
                }
                else
                {
                    GetToWork = false;
                    WeaponManager.Instance.UpdateStatus();
                    gameObject.SetActive(false);
                }
            }
            return;
        }
        // 检测到达目标
        if (isCarryingWood)// 采集逻辑
        {
            DeliverToShip();
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
                if (currentResource != null && OneStep)
                {
                    OneStep = false;
                    WeaponManager.Instance.UpdateStatus();
                    StartCoroutine(HarvestResource());
                }
            }
        }
        
        if (isCarryingWood)
        {
            Collider.isTrigger = false;

        }
        else
        {
            Collider.isTrigger = true;
        }

        //else
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, shipDepositPoint.position, moveSpeed * Time.deltaTime);
        //    if (Vector3.Distance(transform.position, shipDepositPoint.position) < 0.5f)
        //    {
        //        transform.position = shipDepositPoint.position;
        //        gameObject.SetActive(false);
        //    }
        //}



    }

    IEnumerator HarvestResource()
    {
        // 播放砍伐动画
        //GetComponent<Animator>().SetTrigger("Chop");

        yield return new WaitForSeconds(harvestTime);
        currentResource.Amount -= harvestCapacity;
        if (!currentResource.IsEmpty())
        {
            CurHarvestAmount = harvestCapacity;
        }
        else CurHarvestAmount = 1; 
        
        isCarryingWood = true;
        OneStep = true;
    }

    void DeliverToShip()
    {
        if (Vector3.Distance(transform.position, shipDepositPoint.position) < 3f)
        {
            Debug.Log("Deliver to ship");            
            // 将木材存入船只
            ResourceManager.instance.AddResource(CurHarvestAmount, currentResource.type);
            if (currentResource.IsEmpty())
            {
                currentResource = null;
            }
            isCarryingWood = false;
            transform.position = shipDepositPoint.position;
            ChewManager.Instance.ReturnIdleCrew(this);
            if (ChewManager.Instance.ResourceList.Count == 0)
            // 回到闲置状态
            {
                GetToWork = false;
                gameObject.SetActive(false);
            }
            else
            {
                // 继续工作
                if (currentResource == null)
                {
                    AssignTask(ChewManager.Instance.ResourceList[0]);                    
                    Debug.Log("Continue Task");
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, shipDepositPoint.position, moveSpeed * Time.deltaTime);
        }
    }
}