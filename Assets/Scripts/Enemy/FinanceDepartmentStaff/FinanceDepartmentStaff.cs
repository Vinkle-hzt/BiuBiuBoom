using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinanceDepartmentStaff : Enemy
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
    public Transform target;
    private Animator anim;
    public float shootCurTime;

    // TODO：更改子弹
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
        shootCurTime += Time.deltaTime;
        if (shootCurTime > 1 / eInfo.AttackSpeed)
        {
            shootCurTime = 0;
            Attack();
            // TODO: 攻击音乐
        }
    }

    void Attack()
    {
        target = findTarget();

        if (target == null)
            return;
        Vector3 shootDir = (target.position - body.position).normalized;

        // 向三个方向发射子弹
        float cos = Mathf.Cos(5f / 180 * Mathf.PI);
        float sin = Mathf.Sin(5f / 180 * Mathf.PI);

        Matrix4x4 mat1 = new Matrix4x4(
            new Vector4(cos, -sin, 0, 0),
            new Vector4(sin, cos, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );
        Matrix4x4 mat2 = new Matrix4x4(
            new Vector4(cos, sin, 0, 0),
            new Vector4(-sin, cos, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );
        Vector4 tmp = new Vector4(shootDir.x, shootDir.y, shootDir.z, 1);
        Vector4 dir1 = mat1.MultiplyVector(tmp);
        Vector4 dir2 = mat2.MultiplyVector(tmp);

        Transform bullet1 = Instantiate(pfBullet, body.position, Quaternion.identity);
        Transform bullet2 = Instantiate(pfBullet, body.position, Quaternion.identity);
        Transform bullet3 = Instantiate(pfBullet, body.position, Quaternion.identity);

        anim.SetTrigger("Attack");
        bullet1.GetComponent<Bullet>().Setup(new Vector3(dir1.x, dir1.y, 0), eInfo);
        bullet2.GetComponent<Bullet>().Setup(new Vector3(dir2.x, dir2.y, 0), eInfo);
        bullet3.GetComponent<Bullet>().Setup(shootDir, eInfo);
    }
}