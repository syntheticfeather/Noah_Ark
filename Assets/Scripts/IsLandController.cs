using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLandController : MonoBehaviour
{
    public static IsLandController instance;

    public int numberOfIsLand = 12; // 需要生成的岛屿数量
    public float radius = 1000f; // 圆的半径
    public float minDistance = 500f; // 点之间的最小距离
    private List<Vector2> points = new List<Vector2>(); // 存储生成的中心点位置
    public List<GameObject> InitIsLand_List = new List<GameObject>(); // 岛屿预制件列表
    public List<GameObject> CurIsLand_List = new List<GameObject>(); // 当前生成的岛屿列表

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 检查是否有存档数据
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager != null && saveLoadManager.HasSaveData())
        {
            // 加载存档数据
            saveLoadManager.LoadGame();

            // 从SaveLoadManager获取岛屿数据并生成岛屿
            List<IslandData> savedIslands = saveLoadManager.GetIslandsData();
            if (savedIslands != null)
            {
                LoadIslandsFromSave(savedIslands);
            }
        }
        else
        {
            // 随机生成新岛屿
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

    // 登陆或切换到船只
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

    // 登陆,切换镜头移动方式
    public void ToLand()
    {
        CameraFollow.instance.followArk = false;
        CameraFollow.instance.TargetPosition = ArkMovement.Instance.transform.position + new Vector3(0, 0, -10);
        ArkMovement.Instance.rb.velocity = Vector3.zero; // 停靠动画，暂定
    }

    // 切换到船只
    public void ToShip()
    {
        CameraFollow.instance.followArk = true;
    }

    // 随机生成岛屿
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

                // 随机生成岛屿类型
                int islandType = Random.Range(0, InitIsLand_List.Count); // 根据预制件数量随机生成类型

                // 实例化岛屿
                GameObject NewLand = Instantiate(InitIsLand_List[islandType]);
                NewLand.transform.position = newPoint;

                // 设置岛屿类型
                Land islandScript = NewLand.GetComponent<Land>();
                if (islandScript != null)
                {
                    islandScript.type = islandType;
                }

                // 将新岛屿添加到当前岛屿列表中
                CurIsLand_List.Add(NewLand);
            }
            attempts++;
        }

        if (points.Count < numberOfIsLand)
        {
            Debug.LogWarning("Failed to generate all points within the maximum attempts.");
        }
    }

    // 从存档加载岛屿
    public void LoadIslandsFromSave(List<IslandData> savedIslands)
    {
        // 清空现有岛屿
        foreach (var island in CurIsLand_List)
        {
            Destroy(island);
        }
        CurIsLand_List.Clear();
        points.Clear();

        // 根据存档数据生成岛屿
        foreach (var islandData in savedIslands)
        {
            Vector2 newPoint = new Vector2(islandData.position.x, islandData.position.y);
            points.Add(newPoint);

            // 实例化岛屿
            GameObject NewLand = Instantiate(InitIsLand_List[islandData.type]);
            NewLand.transform.position = newPoint;

            // 设置岛屿类型
            Land islandScript = NewLand.GetComponent<Land>();
            if (islandScript != null)
            {
                islandScript.type = islandData.type;
            }

            // 将新岛屿添加到当前岛屿列表中
            CurIsLand_List.Add(NewLand);
        }

        Debug.Log("岛屿已从存档加载");
    }

    // 随机生成圆内的点
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

    // 检查新点是否有效
    bool IsPointValid(Vector2 newPoint)
    {
        // 检查是否离中心太近
        float distance1 = Vector2.Distance(newPoint, Vector2.zero);
        float distance2 = Vector2.Distance(newPoint, PlayerHealthController.instance.transform.position);
        if (distance1 < 100 || distance2 < 50)
        {
            return false;
        }

        // 检查是否离其他点太近
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

    // 更新岛屿状态（超出范围则销毁）
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

    // 打印所有生成的点（用于调试）
    void LogPoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Debug.Log($"Point {i + 1}: ({points[i].x}, {points[i].y})");
        }
    }
}