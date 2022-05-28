using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : Enemy
{
    enum Direction
    {
        Point1,
        Point2
    }

    private Transform body;
    private Transform point1;
    private Transform point2;
    private Direction direction = Direction.Point1;
    private Animator anim;
    public Transform target;
    bool turn = false;
    public float curTime;

    [Header("子弹跟踪时间")]
    public float trackTime;

    [Header("子弹")]
    [SerializeField]
    private Transform pfBullet;
    private void Awake()
    {
        point1 = transform.parent.Find("Point1");
        point2 = transform.parent.Find("Point2");
        body = transform;
        anim = body.GetComponent<Animator>();
    }

    override protected void NormalMovement()
    {
        //AI移动
        //不同敌人有不同的移动方法，此处为父类，实现为空方法
        if (direction == Direction.Point1)
        {
            body.position = Vector3.MoveTowards(body.position, point2.position, eInfo.Speed * Time.deltaTime);
            if (Vector3.Distance(body.position, point2.position) < 0.1f)
            {
                direction = Direction.Point2;
                anim.SetTrigger("Turn");
            }
        }
        else
        {
            body.position = Vector3.MoveTowards(body.position, point1.position, eInfo.Speed * Time.deltaTime);
            if (Vector3.Distance(body.position, point1.position) < 0.1f)
            {
                direction = Direction.Point1;
                anim.SetTrigger("Turn");
            }
        }

        curTime += Time.deltaTime;
        if (curTime > 1 / eInfo.AttackSpeed)
        {
            curTime = 0;
            Attack();
        }
    }

    void Attack()
    {
        Transform bulletTransform = Instantiate(pfBullet, transform.position, Quaternion.identity);

        bulletTransform.GetComponent<BulletDrone>().Setup(target, eInfo, trackTime);
    }
}

