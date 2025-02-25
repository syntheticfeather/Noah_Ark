using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsLandController : MonoBehaviour
{
    public static IsLandController instance;
    public int numberOfIsLand = 12; // ��Ҫ���ɵĵ�������
    public float radius = 1000f; // Բ�İ뾶
    public float minDistance = 500f; // ��֮�����С����
    private List<Vector2> points = new List<Vector2>(); // �洢���ɵ����ĵ�λ��
    public List<GameObject> InitIsLand_List = new List<GameObject>();
    public List<GameObject> CurIsLand_List = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GeneratePoints();
        //LogPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Construction();
        }
    }

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
    //��½,�л���ͷ�ƶ���ʽ��
    public void ToLand()
    {
        CameraFollow.instance.followArk = false;
        CameraFollow.instance.TargetPosition = ArkMovement.Instance.transform.position + new Vector3(0, 0, -10);
        //ͣ���������ݶ�
        ArkMovement.Instance.rb.velocity = Vector3.zero;
    }
    public void ToShip()
    {
        CameraFollow.instance.followArk = true;
    }


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
                GameObject NewLand = Instantiate(InitIsLand_List[Random.Range(0, 6)]);
                NewLand.transform.position = newPoint;
                CurIsLand_List.Add(NewLand);
            }
            attempts++;
        }
        if (points.Count < numberOfIsLand)
        {
            Debug.LogWarning("Failed to generate all points within the maximum attempts.");
        }
    }

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

    bool IsPointValid(Vector2 newPoint)
    {
        float distance1 = Vector2.Distance(newPoint, Vector2.zero);
        if (distance1 < 100)
        {
            return false;
        }
        foreach (Vector2 point in points)
        {
            // ��������֮��ľ���
            float distance = Vector2.Distance(newPoint, point);            
            // �������С����С���룬���µ���Ч
            if (distance < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateIsLand()
    {
        //�������ķ�Χ������
        for (int i = 0; i < CurIsLand_List.Count; i++)
        {
            if (Vector2.Distance(CurIsLand_List[i].transform.position, Vector2.zero) > EventController.instance.Map.transform.localScale.x / 2)
            {
                Destroy(CurIsLand_List[i]);
                CurIsLand_List.Remove(CurIsLand_List[i]);
            }
        }
    }

    void LogPoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Debug.Log($"Point {i + 1}: ({points[i].x}, {points[i].y})");
        }
    }
}