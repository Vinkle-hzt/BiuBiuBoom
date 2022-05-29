using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    private Animator anim;
    [Header("瞄准完成后至射击的间隔时间")]
    public float intervalTime;

    [Header("瞄准目标")]
    public Transform target;

    [Header("子弹")]
    [SerializeField]

    private Transform pfBullet;
    private float shootCurTime;
    private float waitShootTime;
    private bool waitShoot;
    private Vector3 shootDir;
    private float angle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    override protected void NormalMovement()
    {
        // note: 攻击时间 = 原始攻击时间 + 瞄准后至射击的时间
        if (!waitShoot)
        {
            // 瞄准阶段
            shootCurTime += Time.deltaTime;
            shootDir = (target.position - transform.position).normalized;
            angle = 90 - Mathf.Acos(shootDir.x) * Mathf.Rad2Deg;

            if (shootCurTime > 1 / eInfo.AttackSpeed)
            {
                waitShoot = true;
                waitShootTime = 0;
                shootCurTime = 0;
            }
        }
        else
        {
            // 准备射击阶段
            waitShootTime += Time.deltaTime;

            if (waitShootTime > intervalTime)
            {
                // 射击阶段
                waitShoot = false;
                Attack();
            }
        }

        anim.SetFloat("FaceAngle", angle);
    }

    void Attack()
    {
        Transform bulletTransform = Instantiate(pfBullet, transform.position, Quaternion.identity);

        bulletTransform.GetComponent<Bullet>().Setup(shootDir, eInfo);
    }

    public override String GetSkill()
    {
        return "RCE";
    }
}
