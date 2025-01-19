using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChewButton : MonoBehaviour
{    
    public Image Image;
    // Start is called before the first frame update
    void Start()
    {
        //Image.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateText(Sprite sprite)
    {
        Image.sprite = sprite;
    }
}
