using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using BiuBiuBoom.Utils;
using UnityEngine;

public class FlyEnemy : Enemy
{
    enum State
    {
        Wait,
        Aim,
        Shoot
    }
    enum Direction
    {
        Point1,
        Point2
    }

    private Animator anim;

    [Header("瞄准时间")]
    public float doAimTime;

    [Header("瞄准完成后至射击的间隔时间")]
    public float intervalTime;

    [Header("瞄准目标")]
    public Transform target;

    [Header("子弹")]
    [SerializeField]
    private Transform pfBullet;

    [Header("瞄准线")]
    public Transform pfAimLine;

    private float aimTime;
    private float waitTime;
    private float shootTime;
    private Vector3 shootDir;
    private float angle;
    State curState = State.Wait;
    Animator aimAnim;
    Transform body;
    Direction direction = Direction.Point1;
    Transform point1;
    Transform point2;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        pfAimLine = Instantiate(pfAimLine, transform.position, Quaternion.identity);
        aimAnim = pfAimLine.GetComponent<Animator>();
        pfAimLine.gameObject.SetActive(false);
        body = transform;
        point1 = transform.parent.Find("Point1");
        point2 = transform.parent.Find("Point2");
    }

    override protected void NormalMovement()
    {
        if (direction == Direction.Point1)
        {
            body.position = Vector3.MoveTowards(body.position, point2.position, eInfo.Speed * Time.deltaTime);
            if (Vector3.Distance(body.position, point2.position) < 0.1f)
            {
                direction = Direction.Point2;
                if (target == null)
                    body.localScale = new Vector3(-body.localScale.x, body.localScale.y, body.localScale.z);
            }
        }
        else
        {
            body.position = Vector3.MoveTowards(body.position, point1.position, eInfo.Speed * Time.deltaTime);
            if (Vector3.Distance(body.position, point1.position) < 0.1f)
            {
                direction = Direction.Point1;
                if (target == null)
                    body.localScale = new Vector3(-body.localScale.x, body.localScale.y, body.localScale.z);
            }
        }
        if (target != null)
        {
            float dir = target.position.x - body.position.x > 0 ? 1 : -1;
            body.localScale = new Vector3(dir * Mathf.Abs(body.localScale.x), body.localScale.y, body.localScale.z);
        }
    }

    protected override void NormalAttack()
    {
        target = findTarget();

        if (target == null)
        {
            curState = State.Wait;
            waitTime = 0;
            aimTime = 0;
            shootTime = 0;
            aimAnim.SetBool("Flash", false);
            pfAimLine.gameObject.SetActive(false);
            return;
        }


        // note: 攻击时间 = 原始攻击时间 + 瞄准时间 + 瞄准后至射击的时间
        switch (curState)
        {
            case State.Wait:
                Wait();
                waitTime += Time.deltaTime;
                if (waitTime >= 1 / eInfo.AttackSpeed)
                {
                    curState = State.Aim;
                    waitTime = 0;
                }
                break;
            case State.Aim:
                Aim();
                // BgmManager.instance.PlayTurretShoot();
                aimTime += Time.deltaTime;
                if (aimTime >= doAimTime)
                {
                    aimAnim.SetBool("Flash", true);
                    curState = State.Shoot;
                    aimTime = 0;
                }
                break;
            case State.Shoot:
                Shoot();
                anim.Play("RemoteAttack");
                pfAimLine.position = transform.position;
                shootTime += Time.deltaTime;
                if (shootTime >= intervalTime)
                {
                    aimAnim.SetBool("Flash", false);
                    var bullet = Instantiate(pfBullet, transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().Setup(shootDir, eInfo);
                    curState = State.Wait;
                    shootTime = 0;
                }
                break;
        }
    }

    protected override void FallDownAttack()
    {
        pfAimLine.gameObject.SetActive(false);
    }

    void Attack()
    {
        Transform bulletTransform = Instantiate(pfBullet, transform.position, Quaternion.identity);

        bulletTransform.GetComponent<Bullet>().Setup(shootDir, eInfo);
    }

    void Wait()
    {
        shootDir = (target.position - transform.position).normalized;
        angle = 90 - Mathf.Acos(shootDir.x) * Mathf.Rad2Deg;
        pfAimLine.gameObject.SetActive(false);
    }
    void Aim()
    {
        shootDir = (target.position - transform.position).normalized;
        angle = 90 - Mathf.Acos(shootDir.x) * Mathf.Rad2Deg;
        pfAimLine.localScale = new Vector3(10.0f, 0.8f, 1f);
        pfAimLine.position = transform.position;
        pfAimLine.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
        pfAimLine.gameObject.SetActive(true);
    }

    void Shoot()
    {
    }

    override protected void DeadAnim()
    {
        aimAnim.SetBool("Dead", true);
    }

    public override void Dead()
    {
        isDead = true;
        EventHandler.CallEnemyDeathEvent();
        Destroy(transform.parent.gameObject);
    }
}
