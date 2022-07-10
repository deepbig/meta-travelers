using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스의 입력 값을 이용해서 회전한다.
public class CameraRotate : MonoBehaviour
{
    float rx;
    float ry;
    public float rotSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 마우스의 입력 값을 이용해서
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        // rx의 회전 각도를 제한한다 (+- 80도)
        rx = Mathf.Clamp(rx, -80, 80);
        // 2. 회전시킨다.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
        
    }
}
