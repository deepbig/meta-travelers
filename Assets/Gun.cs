using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ray를 이용해서 바라보고 닿은 곳에 총을 쏘고 싶다(총알 자국을 남기고 싶다.)
public class Gun : MonoBehaviour
{
    public ParticleSystem bulletImpact;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // 만약 마우스 왼쪽버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            // 1. 시선을 만들고 (원점, 방향)
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 2. 그 시선을 이용해서 바라봤는데 만약 닿은 곳이 있다면
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 3. 닿은 곳 총알 자국을 가져다 놓고 싶다.
                bulletImpact.transform.position = hitInfo.point;
                // 4. 총알 자국 VFX를 재생하고 싶다.
                bulletImpact.Stop();
                bulletImpact.Play();
                // 5. 총알 자국의 방향을 닿은 곳의 Normal방향으로 회정하고 싶다.
                // 총알 자국의 forward 방향과 닿은 곳의 Normal 방향을 일치시킨다.
                bulletImpact.transform.forward = hitInfo.normal;

                // 만약 hitInfo가 Enemy 컴포넌트를 가지고 있으면 enemy의 AddDamage 함수를 호출
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.AddDamage(1);
                }
            }
        }
    }
}
