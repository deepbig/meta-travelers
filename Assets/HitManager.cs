using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태어날 때 imageHit를 보이지 않게 하고 싶다.
// 적이 플레이어를 공격했을 때 imageHit를 깜빡이게 하고 싶다.
public class HitManager : MonoBehaviour
{
    public static HitManager instance;

    // 처음에 객체가 만들어진 다음에 그 객체가 instance에 담김.
    // HitManager의 instance = HitManager component (Singleton)
    private void Awake()
    {
        instance = this;
    }
    public GameObject imageHit;

    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 imageHit를 보이지 않게 하고 싶다.
        imageHit.SetActive(false);
    }

    public void Hit()
    {
        StartCoroutine("IeHit");
    }

    IEnumerator IeHit()
    {
        // 적이 플레이어를 공격했을 때 imageHit를 깜빡이게 하고 싶다.
        // 0.1초 후에 imageHit를 안보이게 하고 싶다.
        imageHit.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        imageHit.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }
}
