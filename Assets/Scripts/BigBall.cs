using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BigBall : MonoBehaviour {

    Rigidbody2D bigBallBody;
    public Transform bigStand;
    public float moveSpeed = 150f;  // 大球比较灵敏
    public float upSpeed = 300f;
    // 限速！呸！
    public float maxSpeed = 5f;


    Vector2 tmp;
    Vector2 tmpV;
    bool aDown;
    bool dDown;

    
    // F = c - n * d (通过该算式确定上升要施加的力)
    public Transform small;
    public float c = 300f;
    public float n = 50f;
    public float maxDis = 3f;
    public float distance;
    

    // Use this for initialization
    void Start () {
        bigBallBody = GetComponent<Rigidbody2D>();
        tmp = new Vector2();
        tmp = new Vector2();
        aDown = false;
        dDown = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        distance = Vector2.Distance(transform.position, small.position);
        if (distance < maxDis && bigBallBody.velocity.y < maxSpeed)
        {
            bigBallBody.AddForce((c - n * distance) * transform.up * Time.deltaTime);
        }
        // 下落速度不要过快
        if (bigBallBody.velocity.y < -1 * maxSpeed)
        {
            tmp.x = bigBallBody.velocity.x;
            tmp.y = -1 * maxSpeed;
            bigBallBody.velocity = tmp;
        }


        // 强制限制不掉下去以及不旋转
        if (transform.position.y <= bigStand.position.y)
        {
            tmp.x = transform.position.x;
            tmp.y = bigStand.position.y;
            transform.position = tmp;
            tmpV.x = bigBallBody.velocity.x;
            tmpV.y = 0;
            bigBallBody.velocity = tmpV;
        }
        if(transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // 控制 A D 控制大球
        if (Input.GetKeyDown(KeyCode.A))
        {
            aDown = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dDown = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            aDown = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            dDown = false;
        }

        if (aDown && bigBallBody.velocity.x >= -1 * maxSpeed)
        {
            bigBallBody.AddForce(-1 * transform.right * moveSpeed * Time.deltaTime);
        }
        if (dDown && bigBallBody.velocity.x <= maxSpeed)
        {
            bigBallBody.AddForce(transform.right * moveSpeed * Time.deltaTime);
        }

	}

}
