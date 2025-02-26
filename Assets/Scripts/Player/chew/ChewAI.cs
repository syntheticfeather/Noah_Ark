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

    private void OnMouseDown()// 传递其标识符
    {
        ChewBuyUI.instance.CurChewindex = ChewManager.Instance.CrewsToBuy.IndexOf(this);// 传递表示符
        if (ChewBuyUI.instance.gameObject.activeSelf) //显示其UI。
        {
            ChewBuyUI.instance.gameObject.SetActive(false);
        }
        else
        {
            ChewBuyUI.instance.gameObject.SetActive(true);
            ChewBuyUI.instance.ShowData();
        }
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
                ChewManager.Instance.ReturnIdleCrew(this);
                gameObject.SetActive(false);
                Debug.Log(ChewManager.Instance.idleCrews.Count);
            }
            return;
        }
        if (!isCarryingWood && (currentResource == null || currentResource.Amount <= 0))// 物资采集完提前回船。
        {
            transform.position = Vector3.MoveTowards(transform.position, shipDepositPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, shipDepositPoint.position) < 5f)
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
                    ChewManager.Instance.ReturnIdleCrew(this);
                    Debug.Log(ChewManager.Instance.idleCrews.Count);
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
    }

    IEnumerator HarvestResource()
    {
        // 播放砍伐动画
        //GetComponent<Animator>().SetTrigger("Chop");
        SFXManager.instance.soundEffects[3 + currentResource.type].Play();
        Debug.Log("Harvest Music`");
        yield return new WaitForSeconds(harvestTime);
        SFXManager.instance.soundEffects[3 + currentResource.type].Stop();
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
            //Debug.Log("Deliver to ship");            
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
                ChewManager.Instance.ReturnIdleCrew(this);
                Debug.Log(ChewManager.Instance.idleCrews.Count);
            }
            else
            {
                // 继续工作
                if (currentResource == null)
                {
                    AssignTask(ChewManager.Instance.ResourceList[0]);                    
                    //Debug.Log("Continue Task");
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, shipDepositPoint.position, moveSpeed * Time.deltaTime);
        }
    }
}