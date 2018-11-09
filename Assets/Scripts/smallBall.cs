using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class smallBall : MonoBehaviour {

    Rigidbody2D smallBallBody;
    public Transform smallStand;
    public float moveSpeed = 30f;   // 我记得好像小球没有大球那么灵敏
    public float upSpeed = 150f;
    public float maxSpeed = 5f;

    Vector2 tmp;
    Vector2 tmpV;
    Vector2 toBig;
    bool leftDown;
    bool rightDown;
    

    // F = c - n * d (通过该算式确定上升要施加的力)
    public Transform big;
    public float c = 300f;
    public float n = 50f;
    public float maxDis = 3f;
    public float distance;

    // 专门控制小球和大球距离过大的时候小球往大球身上靠的过程
    public float tooFar = 5f;
    public float factor = 50f;



    // Use this for initialization
    void Start () {
        smallBallBody = GetComponent<Rigidbody2D>();
        tmp = new Vector2();
        tmpV = new Vector2();
        toBig = new Vector2();
        leftDown = false;
        rightDown = false;
    }

    // Update is called once per frame
    void Update () {

        distance = Vector2.Distance(transform.position, big.position);
        if (distance < maxDis && smallBallBody.velocity.y < maxSpeed)
        {
            smallBallBody.AddForce((c - n * distance) * transform.up * Time.deltaTime);
        }
        // 下落速度不要过快
        if (smallBallBody.velocity.y < -1 * maxSpeed)
        {
            tmp.x = smallBallBody.velocity.x;
            tmp.y = -1 * maxSpeed;
            smallBallBody.velocity = tmp;
        }

        // 强制限制不掉下去以及不旋转
        // 千万千万要改速度不要强上！...强扭的瓜不甜！(确信
        if (transform.position.y <= smallStand.position.y)  // 直接用数值也可以 当初写的时候我睿智了...
        {
            tmp.x = transform.position.x;
            tmp.y = smallStand.position.y;
            transform.position = tmp;
            tmpV.x = smallBallBody.velocity.x;
            tmpV.y = 0;
            smallBallBody.velocity = tmpV;
        }
        if (transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        // 小球离大球太远的话，强制靠近一些
        if (distance > tooFar && Math.Abs(smallBallBody.velocity.x) < maxSpeed)
        {
            toBig = (big.position - transform.position).normalized;
            smallBallBody.AddForce(toBig * factor * Time.deltaTime);
        }

        // 控制<- -> 控制小球
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftDown = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightDown = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftDown = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightDown = false;
        }

        if (leftDown && smallBallBody.velocity.x >= -1 * maxSpeed)
        {
            smallBallBody.AddForce(-1 * transform.right * moveSpeed * Time.deltaTime);
        }
        if (rightDown && smallBallBody.velocity.x <= maxSpeed)
        {
            smallBallBody.AddForce(transform.right * moveSpeed * Time.deltaTime);
        }

    }
    

    

}
