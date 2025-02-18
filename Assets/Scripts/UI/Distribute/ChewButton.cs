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
        if (Image.sprite == null)
        {
            Image.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateText(Sprite sprite)
    {
        Image.sprite = sprite;
        if (Image.sprite == null)
        {
            Image.gameObject.SetActive(false);
            return;
        }
        Image.gameObject.SetActive(true);
        //Debug.Log("UpdateText");
    }
}
