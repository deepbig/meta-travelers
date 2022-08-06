using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일정 시간마다 적 공장에서 적을 생성해서 내 위치에 가져다 놓고 싶다.
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject enemyFactory;
    public float createTime = 2;
    int count;
    public int COUNT
    {
        get { return count; }
        set
        {
            count = value;
            if (count < 0)
            {
                count = 0;
            }
            else if (count > maxCount)
            {
                count = maxCount;
            }
        }
    }
    public int maxCount = 5;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            if (count < maxCount)
            {
                // 1. 적 공장에서 적을 생성.
                GameObject enemy = Instantiate(enemyFactory);
                // 2. 내 위치에 가져다 놓고 싶다.
                enemy.transform.position =
                    transform.position + new Vector3(Random.value * 2, 0, Random.value * 2);
                // 3. 내 방향과 일치시키고 싶다.
                enemy.transform.rotation = transform.rotation;
                count++;
            }
            // 4. 생성 시간동안 대기하고 싶다.
            yield return new WaitForSeconds(createTime);
        }
    }

    // Update is called once per frame
    void Update() { }
}
