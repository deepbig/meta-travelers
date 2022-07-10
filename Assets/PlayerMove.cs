using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 입력에 따라 앞, 뒤, 좌, 우로 움직인다.
// 사용자의 입력에 따라 점프시킨다. (중력, 뛰는 힘, y 속도)
public class PlayerMove : MonoBehaviour
{
    // - 크기
    public float speed = 5;
    public float gravity = -9.81f;
    public float jumpPower = 10;
    float yVelocity;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // y속도에 중력을 계속 더한다.
        yVelocity += gravity * Time.deltaTime;
        // 만약 사용자가 전프 버튼을 누르면 y속도에 뛰는 힘을 대입한다.
        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }

        // 사용자의 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 방향을 만들고
        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        // 카메라가 바라보는 방향을 앞 방향으로 변경
        dir = Camera.main.transform.TransformDirection(dir);

        // dir의 크기를 1로 만든다.
        dir.Normalize();
        // y속도를 최종 dir의 y에 대입한다.
        dir.y = yVelocity;

        // 그 방향을 이동시킨다. P = P0 + vt
        cc.Move(dir * speed * Time.deltaTime);
    }
}
