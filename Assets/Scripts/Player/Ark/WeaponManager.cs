using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StringGameObjectPair
{
    public string key;
    public GameObject value;
}
public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public List<GameObject> WeaponList = new List<GameObject>();
    public List<GameObject> AnimalList = new List<GameObject>();
    public List<StringGameObjectPair> BulletList = new List<StringGameObjectPair>();


    // ����һ���ֵ����ڿ��ٲ���
    private Dictionary<string, GameObject> bulletDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Instance = this;
        // ���б�ת��Ϊ�ֵ�
        foreach (var pair in BulletList)
        {
            if (!bulletDict.ContainsKey(pair.key))
            {
                bulletDict[pair.key] = pair.value;
            }
            else
            {
                Debug.LogWarning($"Duplicate key found: {pair.key}");
            }
        }
    }
    // ͨ�������Ҷ�Ӧ��GameObject
    public GameObject GetBulletByString(string key)
    {
        if (bulletDict.TryGetValue(key, out GameObject bullet))
        {
            return bullet;
        }
        else
        {
            Debug.LogWarning($"No bullet found for key: {key}");
            return null;
        }
    }

    public void UpdateStatus()// ���ڹ���ʱȡ����������
    {
        for (int i = 0; i < AnimalList.Count; i++)
        {
            if (AnimalList[i])
            {
                if (AnimalList[i].GetComponent<ChewAI>().GetToWork)// ȥ�������ܹ���
                {
                    if (AnimalList[i].name == "Noah")
                    {
                        WeaponList[i].GetComponent<PlayerWeapon>().CanAtk = false;
                    }
                    else
                    {
                        WeaponList[i].GetComponent<TurretAI>().CanAtk = false;
                    }
                }
                else
                {
                    if (AnimalList[i].name == "Noah")
                    {
                        WeaponList[i].GetComponent<PlayerWeapon>().CanAtk = true;
                    }
                    else
                    {
                        WeaponList[i].GetComponent<TurretAI>().CanAtk = true;
                    }
                }
            }
        }
    }
}
