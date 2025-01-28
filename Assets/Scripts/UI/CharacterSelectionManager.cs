using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    //// 选择角色界面的引用，通常是一个包含所有可选角色头像的Panel
    //public GameObject selectionPanel;

    //// 字典用于跟踪每个角色是否已被选择的状态
    //private Dictionary<string, bool> characterSelectedStatus = new Dictionary<string, bool>();

    //// 字典用于跟踪每个目标方框（Image）中放置的角色名称
    //private Dictionary<Image, string> assignedCharacters = new Dictionary<Image, string>();

    //// 初始化角色选择状态，设置所有角色初始未被选择
    //void Start()
    //{
    //    // 假设你有一个字符串数组或列表存储所有角色的名字
    //    foreach (var charName in /* 这里填写你的角色名字列表 */)
    //    {
    //        // 将每个角色名初始化为未选择状态(false)
    //        characterSelectedStatus[charName] = false;
    //    }
    //}

    //// 处理角色选择的函数，当用户从选择界面点击某个角色时调用此函数
    //// 参数characterName是被选择角色的名字，targetBox是需要更新显示角色头像的目标方框
    //public void OnCharacterSelected(string characterName, Image targetBox)
    //{
    //    // 如果该角色尚未被选择
    //    if (!characterSelectedStatus[characterName])
    //    {
    //        // 更新角色的选择状态为已选择
    //        characterSelectedStatus[characterName] = true;

    //        // 更新目标方框的图像为所选角色的头像
    //        // 注意：你需要实现获取对应角色头像Sprite的逻辑
    //        targetBox.sprite = /* 获取对应角色头像的Sprite */;
    //        assignedCharacters[targetBox] = characterName; // 记录哪个方框分配给了哪个角色

    //        // 检查是否有其他方框已经选择了同一个角色，并重置它
    //        foreach (var kvp in assignedCharacters)
    //        {
    //            if (kvp.Value == characterName && kvp.Key != targetBox)
    //            {
    //                ResetBox(kvp.Key);
    //            }
    //        }
    //    }
    //    // 关闭选择角色界面
    //    selectionPanel.SetActive(false);
    //}

    //// 重置指定方框的函数，将方框的图像重置为空，并释放该角色的选择状态
    //private void ResetBox(Image box)
    //{
    //    box.sprite = null; // 清除方框中的图像
    //    string previousCharacter = assignedCharacters[box]; // 获取之前分配给这个方框的角色名
    //    characterSelectedStatus[previousCharacter] = false; // 设置该角色为未选择状态
    //    assignedCharacters.Remove(box); // 从字典中移除这个方框的记录
    //}
}