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
    public bool IsBought;
    CircleCollider2D Collider;
    private void Awake()
    {
        Collider = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        harvestCapacity = (int)GetComponent<Chew>().Stats.Maxtake;
    }

    private void OnMouseDown()// �������ʶ��
    {
        ChewBuyUI.instance.CurChewindex = ChewManager.Instance.CrewsToBuy.IndexOf(this);// ���ݱ�ʾ��
        if (ChewBuyUI.instance.BuyUI.activeSelf) //��ʾ��UI��
        {
            ChewBuyUI.instance.BuyUI.SetActive(false);
        }
        else
        {
            ChewBuyUI.instance.BuyUI.SetActive(true);
            ChewBuyUI.instance.ShowData();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChewBuyUI.instance.BuyUI.SetActive(false);
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
        if (!IsBought)
        {
            gameObject.SetActive(true);
            return;
        }
        if (DirectToShip)// G��ǿ�ƻش�
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
        if (!isCarryingWood && (currentResource == null || currentResource.Amount <= 0))// ���ʲɼ�����ǰ�ش���
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
        // ��⵽��Ŀ��
        if (isCarryingWood)// �ɼ��߼�
        {
            DeliverToShip();
        }
        else
        {
            if (Vector3.Distance(transform.position, Target) > 0.5f)
            {
                // �ƶ���Ŀ�꣬���õ���            
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
        // ���ſ�������
        //GetComponent<Animator>().SetTrigger("Chop");
        SFXManager.instance.soundEffects[currentResource.type].clip = SFXManager.instance.sound[3+ currentResource.type];
        SFXManager.instance.soundEffects[currentResource.type].Play();
        Debug.Log("Harvest Music`");
        yield return new WaitForSeconds(harvestTime);
        SFXManager.instance.soundEffects[currentResource.type].Stop();
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
            // ��ľ�Ĵ��봬ֻ
            ResourceManager.instance.AddResource(CurHarvestAmount, currentResource.type);
            if (currentResource.IsEmpty())
            {
                currentResource = null;
            }
            isCarryingWood = false;
            transform.position = shipDepositPoint.position;
            ChewManager.Instance.ReturnIdleCrew(this);
            if (ChewManager.Instance.ResourceList.Count == 0)
            // �ص�����״̬
            {
                GetToWork = false;
                gameObject.SetActive(false);
                ChewManager.Instance.ReturnIdleCrew(this);
                Debug.Log(ChewManager.Instance.idleCrews.Count);
            }
            else
            {
                // ��������
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