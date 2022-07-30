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
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void NormalMovement()
    {
        if (curState == State.Move)
            target = findTarget();

        if (target != null)
        {
            switch (curState)
            {
                case State.Move:
                    float diff = target.position.x - transform.position.x;
                    if (Mathf.Abs(diff) > 0.01f)
                        moveDir = diff > 0 ? 1 : -1;
                    transform.localScale = new Vector3(moveDir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    if (Vector3.Distance(transform.position, target.position) > attackDistance)
                    {
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
                        anim.Play("CloseAttack");
                        // TODO: 蓄力动画，蓄力音效
                    }
                    break;
                case State.Attack:
                    Attack();
                    if (moveDistance <= 0)
                    {
                        curState = State.Move;
                        chargeTime = 0;
                        anim.Play("CloseIdle");
                    }
                    break;
            }
        }
    }

    void Attack()
    {
        float curMove = Mathf.Min(moveDir * eInfo.Speed * Time.deltaTime * 5, moveDistance);
        moveDistance -= Mathf.Abs(curMove);
        transform.position += new Vector3(curMove, 0, 0);
        // TODO: 攻击动画，攻击音效
        // TODO: 攻击判定
    }

    public void Hit(InfoController player)
    {
        if (curState == State.Attack)
        {
            eInfo.TakeDamage(eInfo, player);
        }
    }
}
