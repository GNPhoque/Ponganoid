using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    
    //Local variables
    public float speed = 7f;
    RectTransform border;
    Vector3 direction;
    Vector3 stageDimensions;
    Vector3 stageTop;
    public event Action OnLostBall;
    public event Action OnPlayerHit;
    public event Action OnBlockHit;
    public event Action OnEnnemyHit;

    void Start ()
    {   //Get top Position for collision check
        border = GameObject.Find("Top Border").GetComponent<RectTransform>();
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        stageTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - new Vector3(0, 2 * border.rect.height * border.lossyScale.y, 0);
        //Set initial ball direction
        direction = new Vector3(1, 0, 0).normalized;
    }
	
	// Update is called once per frame
	void Update ()
    {    //move in direction
        transform.position += direction * speed * Time.deltaTime; 
        if (transform.position.y > stageTop.y) //check if over borders
        {
            direction.y = -direction.y; // if upper wall hit, bounce down
        }
        else if (transform.position.y < -stageTop.y) //check if under borders
        {
            direction.y = -direction.y; // if bottom wall hit, bounce up
        }
        if (stageDimensions.x < transform.position.x || transform.position.x < -(stageDimensions.x)) 
        {
            Destroy(gameObject); //if out of board, destroy ball and call events
            if (OnLostBall != null)
            {
                OnLostBall();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            direction.x = -direction.x;
            direction.y=(transform.position.y-collider.transform.position.y)/collider.collider.bounds.size.y;
            //direction.Normalize();
            if (OnPlayerHit != null)
            {
                OnPlayerHit();
            }
        }
        if (collider.gameObject.tag == "Block")
        {
            if (OnBlockHit != null)
            {
                OnBlockHit();
            }
        }
        if (collider.gameObject.tag == "Ennemy")
        {
            if (OnEnnemyHit != null)
            {
                OnEnnemyHit();
            }
        }
    }
}
