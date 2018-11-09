using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform small;
    public Transform big;
    public float scaleX = 6f;
    public float scaleY = 1.5f;
    public float scaleXmin = 3f;
    public float scaleYmin = 0.5f;
    public float moveX = 7.2f;
    public float moveY = 2f;
    public float cameraSpeed = 100f;
    public float scaleSpeed = 30f;

    Camera mainCamera;

    Vector2 smallP, bigP, cameraP;
    Vector3 tmp;
    Vector3 currentScale;

	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Camera>();
        tmp = new Vector3(0, 0, -10);
	}
	
	// Update is called once per frame
	void Update () {
        smallP = small.position * (5 / mainCamera.orthographicSize);
        bigP = big.position * (5 / mainCamera.orthographicSize);
        cameraP = transform.position * (5 / mainCamera.orthographicSize);
        /************************************************ 缩放 ********************************************/
        if ((smallP.x - cameraP.x > scaleX && cameraP.x - bigP.x > scaleX) ||
            (bigP.x - cameraP.x > scaleX && cameraP.x - smallP.x > scaleX) ||
            (smallP.y - cameraP.y > scaleY && cameraP.y - bigP.y > scaleY) ||
            (bigP.y - cameraP.y > scaleY && cameraP.y - smallP.y > scaleY))
        {
            mainCamera.orthographicSize += scaleSpeed * Time.deltaTime;
        }
        // 王足各是个懒人，她懒得写反面情况了，直接用正面的反面了
        if ((smallP.x - cameraP.x > scaleXmin && cameraP.x - bigP.x > scaleXmin) ||
            (bigP.x - cameraP.x > scaleXmin && cameraP.x - smallP.x > scaleXmin) ||
            (smallP.y - cameraP.y > scaleYmin && cameraP.y - bigP.y > scaleYmin) ||
            (bigP.y - cameraP.y > scaleYmin && cameraP.y - smallP.y > scaleYmin))
        {
            //pass
        }
        else
        {
            if (mainCamera.orthographicSize > 5f)
            {
                mainCamera.orthographicSize -= scaleSpeed * Time.deltaTime;
            }
        }


        /************************************** 移动 **************************************/
        if (smallP.x - cameraP.x > moveX || bigP.x - cameraP.x > moveX)
        {
            tmp.x = transform.position.x + cameraSpeed * Time.deltaTime;
            tmp.y = transform.position.y;
            transform.position = tmp;
        }
        else if (cameraP.x - smallP.x > moveX || cameraP.x - bigP.x > moveX)
        {
            tmp.x = transform.position.x - cameraSpeed * Time.deltaTime;
            tmp.y = transform.position.y;
            transform.position = tmp;
        }
        else if (smallP.y - cameraP.y > moveY || bigP.y - cameraP.y > moveY)
        {
            tmp.x = transform.position.x;
            tmp.y = transform.position.y + cameraSpeed * Time.deltaTime;
            transform.position = tmp;
        }
        else if (cameraP.y - smallP.y > moveY || cameraP.y - bigP.y > moveY)
        {
            tmp.x = transform.position.x;
            tmp.y = transform.position.y - cameraSpeed * Time.deltaTime;
            transform.position = tmp;
        }

    }
}
