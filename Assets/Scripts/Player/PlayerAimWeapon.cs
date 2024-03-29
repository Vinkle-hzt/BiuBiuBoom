﻿using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;

public class PlayerAimWeapon : MonoBehaviour
{

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public InfoController playerInfo;
    }

    [SerializeField]
    [Header("攻击间隔")]
    private float attackTime;
    private Transform aimTransform;
    // private Transform aimGunEndPointTransform; // 子弹发射位置
    private Transform[] shootPoints = new Transform[5]; // 发射点
    private Transform bodyTransform;
    private Animator animator;
    private Animator playerAnim;
    [SerializeField] private float curTime;
    private InfoController pInfo;
    private Rigidbody2D rb;
    private int curPoint = 0;
    private void Start()
    {
        aimTransform = transform.Find("Aim");
        // aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        // animator = aimTransform.Find("Visual").GetComponent<Animator>();
        // bodyTransform = transform.Find("Body");

        for (int i = 0; i < 5; i++)
            shootPoints[i] = aimTransform.Find("ShootPoint" + (i + 1));
        playerAnim = transform.Find("Body").GetComponent<Animator>();
        pInfo = transform.GetComponent<InfoController>();
        attackTime = 1.0f / pInfo.AttackSpeed;
        curTime = attackTime;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // HandleAiming();
        HandleShooting();
    }

    // private void HandleAiming()
    // {
    //     Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

    //     Vector3 aimDirection = (mouseWorldPosition - aimTransform.position).normalized;
    //     float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
    //     aimTransform.eulerAngles = new Vector3(0, 0, angle);

    //     Vector3 aimLocalScale = Vector3.one;
    //     if (Mathf.Abs(angle) > 90)
    //         aimLocalScale.y = -1f;
    //     else
    //         aimLocalScale.y = 1f;

    //     aimTransform.localScale = aimLocalScale;
    //     setPlayerLookAt(mouseWorldPosition);
    // }


    private void HandleShooting()
    {
        curTime += Time.deltaTime;
        if (pInfo.CanShoot && Input.GetKeyDown(InputController.instance.fire))
        {
            if (curTime >= attackTime)
            {
                curTime = 0;
                Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
                // animator.SetTrigger("Shoot");
                playerAnim.SetTrigger("Attack");
                BgmManager.instance.PlayPlayerShoot();
                OnShoot?.Invoke(this, new OnShootEventArgs
                {
                    gunEndPointPosition = shootPoints[curPoint].position,
                    shootPosition = mousePosition,
                    playerInfo = pInfo
                });
                curPoint = (curPoint + 1) % 5;
            }
        }
    }

    // // 使人物和鼠标方向相同
    // private void setPlayerLookAt(Vector3 position)
    // {
    //     float lookAtDir = (position.x - bodyTransform.position.x) < 0 ? 1 : -1;
    //     Vector3 localScale = bodyTransform.localScale;

    //     if (localScale.x * lookAtDir < 0)
    //         bodyTransform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);


    //     if (rb.velocity.x * bodyTransform.localScale.x >= 0)
    //         EventHandler.CallAnimationReversePlay(true);
    //     else
    //         EventHandler.CallAnimationReversePlay(false);
    // }
}
