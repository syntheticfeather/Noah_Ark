using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLandController : MonoBehaviour
{
    public static IsLandController instance;

    public int numberOfIsLand = 12; // ��Ҫ���ɵĵ�������
    public float radius = 1000f; // Բ�İ뾶
    public float minDistance = 500f; // ��֮�����С����
    private List<Vector2> points = new List<Vector2>(); // �洢���ɵ����ĵ�λ��
    public List<GameObject> InitIsLand_List = new List<GameObject>(); // ����Ԥ�Ƽ��б�
    public List<GameObject> CurIsLand_List = new List<GameObject>(); // ��ǰ���ɵĵ����б�

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // ����Ƿ��д浵����
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager != null && saveLoadManager.HasSaveData())
        {
            // ���ش浵����
            saveLoadManager.LoadGame();

            // ��SaveLoadManager��ȡ�������ݲ����ɵ���
            List<IslandData> savedIslands = saveLoadManager.GetIslandsData();
            if (savedIslands != null)
            {
                LoadIslandsFromSave(savedIslands);
            }
        }
        else
        {
            // ��������µ���
            GeneratePoints();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Construction();
        }
    }

    // ��½���л�����ֻ
    public void Construction()
    {
        foreach (var Island in CurIsLand_List)
        {
            if (Island.GetComponent<Land>().CanLand)
            {
                if (CameraFollow.instance.followArk)
                {
                    ToLand();
                }
                else
                {
                    ToShip();
                }
            }
        }
        foreach (var Island in InitIsLand_List)
        {
            if (Island.GetComponent<Land>().CanLand)
            {
                if (CameraFollow.instance.followArk)
                {
                    ToLand();
                }
                else
                {
                    ToShip();
                }
            }
        }
    }

    // ��½,�л���ͷ�ƶ���ʽ
    public void ToLand()
    {
        CameraFollow.instance.followArk = false;
        CameraFollow.instance.TargetPosition = ArkMovement.Instance.transform.position + new Vector3(0, 0, -10);
        ArkMovement.Instance.rb.velocity = Vector3.zero; // ͣ���������ݶ�
    }

    // �л�����ֻ
    public void ToShip()
    {
        CameraFollow.instance.followArk = true;
    }

    // ������ɵ���
    void GeneratePoints()
    {
        int maxAttempts = 1000; // ����Դ�������������ѭ��
        int attempts = 0;

        while (points.Count < numberOfIsLand && attempts < maxAttempts)
        {
            // �������һ����
            Vector2 newPoint = GetRandomPointInCircle(radius);

            // ����µ������е�ľ���
            if (IsPointValid(newPoint))
            {
                points.Add(newPoint);

                // ������ɵ�������
                int islandType = Random.Range(0, InitIsLand_List.Count); // ����Ԥ�Ƽ����������������

                // ʵ��������
                GameObject NewLand = Instantiate(InitIsLand_List[islandType]);
                NewLand.transform.position = newPoint;

                // ���õ�������
                Land islandScript = NewLand.GetComponent<Land>();
                if (islandScript != null)
                {
                    islandScript.type = islandType;
                }

                // ���µ�����ӵ���ǰ�����б���
                CurIsLand_List.Add(NewLand);
            }
            attempts++;
        }

        if (points.Count < numberOfIsLand)
        {
            Debug.LogWarning("Failed to generate all points within the maximum attempts.");
        }
    }

    // �Ӵ浵���ص���
    public void LoadIslandsFromSave(List<IslandData> savedIslands)
    {
        // ������е���
        foreach (var island in CurIsLand_List)
        {
            Destroy(island);
        }
        CurIsLand_List.Clear();
        points.Clear();

        // ���ݴ浵�������ɵ���
        foreach (var islandData in savedIslands)
        {
            Vector2 newPoint = new Vector2(islandData.position.x, islandData.position.y);
            points.Add(newPoint);

            // ʵ��������
            GameObject NewLand = Instantiate(InitIsLand_List[islandData.type]);
            NewLand.transform.position = newPoint;

            // ���õ�������
            Land islandScript = NewLand.GetComponent<Land>();
            if (islandScript != null)
            {
                islandScript.type = islandData.type;
            }

            // ���µ�����ӵ���ǰ�����б���
            CurIsLand_List.Add(NewLand);
        }

        Debug.Log("�����ѴӴ浵����");
    }

    // �������Բ�ڵĵ�
    Vector2 GetRandomPointInCircle(float radius)
    {
        // ������ɽǶȺͰ뾶
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float r = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

        // ת��Ϊ�ѿ�������
        float x = r * Mathf.Cos(angle);
        float y = r * Mathf.Sin(angle);

        return new Vector2(x, y);
    }

    // ����µ��Ƿ���Ч
    bool IsPointValid(Vector2 newPoint)
    {
        // ����Ƿ�������̫��
        float distance1 = Vector2.Distance(newPoint, Vector2.zero);
        float distance2 = Vector2.Distance(newPoint, PlayerHealthController.instance.transform.position);
        if (distance1 < 100 || distance2 < 50)
        {
            return false;
        }

        // ����Ƿ���������̫��
        foreach (Vector2 point in points)
        {
            float distance = Vector2.Distance(newPoint, point);
            if (distance < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    // ���µ���״̬��������Χ�����٣�
    public void UpdateIsLand()
    {
        for (int i = 0; i < CurIsLand_List.Count; i++)
        {
            if (Vector2.Distance(CurIsLand_List[i].transform.position, Vector2.zero) > EventController.instance.Map.transform.localScale.x / 2)
            {
                Destroy(CurIsLand_List[i]);
                CurIsLand_List.Remove(CurIsLand_List[i]);
            }
        }
    }

    // ��ӡ�������ɵĵ㣨���ڵ��ԣ�
    void LogPoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Debug.Log($"Point {i + 1}: ({points[i].x}, {points[i].y})");
        }
    }
}