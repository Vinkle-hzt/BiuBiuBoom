using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionDepartmentStaff : Enemy
{
    enum State
    {
        Move,   // 移动
        Charge, // 蓄力
        Attack // 攻击
    }
    [Header("攻击距离")]
    public float attackDistance;

    [Header("冲刺距离")]
    public float flashDistance;

    public Transform target;
    private float chargeTime;
    private State curState = State.Move;
    private float moveDistance;
    private float moveDir;
    protected override void NormalMovement()
    {
        target = findTarget();

        if (target != null)
        {
            switch (curState)
            {
                case State.Move:
                    if (Vector3.Distance(transform.position, target.position) > attackDistance)
                    {
                        moveDir = target.position.x - transform.position.x > 0 ? 1 : -1;
                        transform.position += new Vector3(moveDir * eInfo.Speed * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        chargeTime = 0;
                        curState = State.Charge;
                    }
                    break;
                case State.Charge:
                    chargeTime += Time.deltaTime;
                    if (chargeTime >= 1 / eInfo.AttackSpeed)
                    {
                        moveDistance = flashDistance;
                        curState = State.Attack;
                        // TODO: 蓄力动画，蓄力音效
                    }
                    break;
                case State.Attack:
                    Attack();
                    if (moveDistance <= 0)
                    {
                        curState = State.Move;
                        chargeTime = 0;
                    }
                    break;
            }
        }
    }

    void Attack()
    {
        float curMove = Mathf.Min(moveDir * eInfo.Speed * Time.deltaTime * 10, moveDistance);
        moveDistance -= Mathf.Abs(curMove);
        transform.position += new Vector3(curMove, 0, 0);
        // TODO: 攻击动画，攻击音效
        // TODO: 攻击判定
    }
}
