using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class test : MonoBehaviour
{
    public Button button;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        button.GetComponent<ChewButton>().UpdateText(item.GetComponent<Chew>().sprite);     
    }
}
