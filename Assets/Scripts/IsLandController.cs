using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsLandController : MonoBehaviour
{
    public static IsLandController instance;
    public int numberOfIsLand = 12; // 需要生成的岛屿数量
    public float radius = 1000f; // 圆的半径
    public float minDistance = 500f; // 点之间的最小距离
    private List<Vector2> points = new List<Vector2>(); // 存储生成的中心点位置
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
    //登陆,切换镜头移动方式。
    public void ToLand()
    {
        CameraFollow.instance.followArk = false;
        CameraFollow.instance.TargetPosition = ArkMovement.Instance.transform.position + new Vector3(0, 0, -10);
        //停靠动画，暂定
        ArkMovement.Instance.rb.velocity = Vector3.zero;
    }
    public void ToShip()
    {
        CameraFollow.instance.followArk = true;
    }


    void GeneratePoints()
    {
        int maxAttempts = 1000; // 最大尝试次数，避免无限循环
        int attempts = 0;

        while (points.Count < numberOfIsLand && attempts < maxAttempts)
        {
            // 随机生成一个点
            Vector2 newPoint = GetRandomPointInCircle(radius);
            // 检查新点与已有点的距离
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
        // 随机生成角度和半径
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float r = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

        // 转换为笛卡尔坐标
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
            // 计算两点之间的距离
            float distance = Vector2.Distance(newPoint, point);            
            // 如果距离小于最小距离，则新点无效
            if (distance < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateIsLand()
    {
        //超出岛的范围，销毁
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