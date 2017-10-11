using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour {
    
    //Inspector Field
    RectTransform border;

    //Local Variables
    public float speed = 10f;
    Vector3 stageTop;

    private void Start()
    {   // Get position of the wall for collision check
        border = GameObject.Find("Top Border").GetComponent<RectTransform>();
        stageTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0))-new Vector3(0,2*border.rect.height*border.lossyScale.y,0);
    }
    
    void Update()
    {   //move by input direction
        transform.Translate(new Vector3(-Input.GetAxisRaw("Vertical2P"), 0, 0) * speed * Time.deltaTime);
        if (transform.position.y > stageTop.y - GetComponent<RectTransform>().rect.width) //check if over borders
        {
            transform.position = new Vector3(transform.position.x, stageTop.y - GetComponent<RectTransform>().rect.width, transform.position.z); //set in borders
        }
        if (transform.position.y < -(stageTop.y - GetComponent<RectTransform>().rect.width)) //check if under borders
        {
            transform.position = new Vector3(transform.position.x, -(stageTop.y - GetComponent<RectTransform>().rect.width), transform.position.z); //set in borders
        }
    }
}
