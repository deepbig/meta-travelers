using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// FSM으로 상태를 제어하고 싶다.
// 정지, 이동, 공격, 죽음
public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    public enum State
    {
        Idle,
        Move,
        Attack,
        Die
    }

    public State state;
    PlayerHP php;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.Idle;
        target = GameObject.Find("Player");
        php = target.GetComponent<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Move)
        {
            UpdateMove();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
    }

    GameObject target;
    public float findDistance = 1;

    private void UpdateIdle()
    {
        // target이 감지거리 안에 들어오면 Move로 전이하고 싶다.
        // 1. 나와 target의 거리 구하기
        float distance = Vector3.Distance(transform.position, target.transform.position);
        // 2. 만약 그 거리가 감지거리보다 작다면 Move로 전이하고 싶다.
        if (distance < findDistance)
        {
            state = State.Move;
            agent.destination = target.transform.position; 
        }
    }

    public float speed = 3;
    public float attackDistance = 2;

    private void UpdateMove()
    {
        // target방향으로 이동하다가 target이 공격 거리 안에 들어오면 Attack으로 전이하고 싶다.
        // 1. target 방향으로 이동하고 싶다. P = P0 + vt
        // Vector3 dir = target.transform.position - transform.position;
        // dir.Normalize();
        // transform.position += dir * speed * Time.deltaTime;
        agent.destination = target.transform.position; 

        // 2. 나와 target의 거리를 구해서
        float distance = Vector3.Distance(transform.position, target.transform.position);
        // 3. 만약 그 거리가 공격 거리보다 작다면 Attack으로 전이하고 싶다.
        if (distance < attackDistance)
        {
            state = State.Attack;
            agent.isStopped = true;
        }
    }

    float currentTime;
    float attackTime = 1;

    private void UpdateAttack()
    {
        // 일정 시간마다 공경을 하되 공격 시점에 target이 공격거리 밖에 있으면 Move로 전이하고 싶다.
        // 시간이 흐르다가
        currentTime += Time.deltaTime;
        // 현재 시간이 공격 시간이 되면
        if (currentTime > attackTime)
        {
            // 현재 시간을 초기화하고
            currentTime = 0;
            // 만약 target이 공격거리 밖에 있으면 Move 상태로 전이하고 싶다.
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > attackDistance)
            {
                state = State.Move;
                agent.isStopped = false;
            }
            else
            {
                // 공격 성공
                // 플레이어를 공격하고
                php.AddDamage(1);
                // HitManager의 Hit함수를 호출하고 싶다.
                // HitManager hm = GameObject.Find("HitManager").GetComponent<HitManager>();
                // hm.Hit();
                HitManager.instance.Hit();
            }
        }
    }

    public void AddDamage(int damage)
    {
        agent.isStopped = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemyManager.instance.COUNT--;
    }
}
