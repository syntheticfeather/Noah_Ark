using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noah : MonoBehaviour
{
    public Camera Camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnMouseDrag()
    {
        if (UIController.Instance.ChewManagerPanel.activeSelf == true)
        { 
            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldPosition = Camera.ScreenToWorldPoint(mousePosition);
            transform.position = worldPosition;            
        }
    }
}


