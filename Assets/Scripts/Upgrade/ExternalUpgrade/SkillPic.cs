using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPic : MonoBehaviour
{
    public Image SkillImage;
    public List<Sprite> Image;  
    public TMP_Text SkillCost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Changeimage(int index)
    {
        SkillImage.sprite = Image[index];
    }
}
