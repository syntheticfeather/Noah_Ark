using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    //// ѡ���ɫ��������ã�ͨ����һ���������п�ѡ��ɫͷ���Panel
    //public GameObject selectionPanel;

    //// �ֵ����ڸ���ÿ����ɫ�Ƿ��ѱ�ѡ���״̬
    //private Dictionary<string, bool> characterSelectedStatus = new Dictionary<string, bool>();

    //// �ֵ����ڸ���ÿ��Ŀ�귽��Image���з��õĽ�ɫ����
    //private Dictionary<Image, string> assignedCharacters = new Dictionary<Image, string>();

    //// ��ʼ����ɫѡ��״̬���������н�ɫ��ʼδ��ѡ��
    //void Start()
    //{
    //    // ��������һ���ַ���������б�洢���н�ɫ������
    //    foreach (var charName in /* ������д��Ľ�ɫ�����б� */)
    //    {
    //        // ��ÿ����ɫ����ʼ��Ϊδѡ��״̬(false)
    //        characterSelectedStatus[charName] = false;
    //    }
    //}

    //// �����ɫѡ��ĺ��������û���ѡ�������ĳ����ɫʱ���ô˺���
    //// ����characterName�Ǳ�ѡ���ɫ�����֣�targetBox����Ҫ������ʾ��ɫͷ���Ŀ�귽��
    //public void OnCharacterSelected(string characterName, Image targetBox)
    //{
    //    // ����ý�ɫ��δ��ѡ��
    //    if (!characterSelectedStatus[characterName])
    //    {
    //        // ���½�ɫ��ѡ��״̬Ϊ��ѡ��
    //        characterSelectedStatus[characterName] = true;

    //        // ����Ŀ�귽���ͼ��Ϊ��ѡ��ɫ��ͷ��
    //        // ע�⣺����Ҫʵ�ֻ�ȡ��Ӧ��ɫͷ��Sprite���߼�
    //        targetBox.sprite = /* ��ȡ��Ӧ��ɫͷ���Sprite */;
    //        assignedCharacters[targetBox] = characterName; // ��¼�ĸ������������ĸ���ɫ

    //        // ����Ƿ������������Ѿ�ѡ����ͬһ����ɫ����������
    //        foreach (var kvp in assignedCharacters)
    //        {
    //            if (kvp.Value == characterName && kvp.Key != targetBox)
    //            {
    //                ResetBox(kvp.Key);
    //            }
    //        }
    //    }
    //    // �ر�ѡ���ɫ����
    //    selectionPanel.SetActive(false);
    //}

    //// ����ָ������ĺ������������ͼ������Ϊ�գ����ͷŸý�ɫ��ѡ��״̬
    //private void ResetBox(Image box)
    //{
    //    box.sprite = null; // ��������е�ͼ��
    //    string previousCharacter = assignedCharacters[box]; // ��ȡ֮ǰ������������Ľ�ɫ��
    //    characterSelectedStatus[previousCharacter] = false; // ���øý�ɫΪδѡ��״̬
    //    assignedCharacters.Remove(box); // ���ֵ����Ƴ��������ļ�¼
    //}
}