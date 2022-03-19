﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    [Header("攻击间隔")]
    public float attackTime = 0.5f;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    private Transform aimTransform;
    private Transform aimGunEndPointTransform; // 子弹发射位置
    private Animator animator;
    [SerializeField] private float curTime;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        animator = aimTransform.Find("Visual").GetComponent<Animator>();
        curTime = attackTime;
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();

    }

    private void HandleAiming()
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mouseWorldPosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (Mathf.Abs(angle) > 90)
            aimLocalScale.y = -1f;
        else
            aimLocalScale.y = 1f;

        aimTransform.localScale = aimLocalScale;
    }


    private void HandleShooting()
    {
        curTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (curTime >= attackTime)
            {
                curTime = 0;
                Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
                animator.SetTrigger("Shoot");
                OnShoot?.Invoke(this, new OnShootEventArgs
                {
                    gunEndPointPosition = aimGunEndPointTransform.position,
                    shootPosition = mousePosition,
                });
            }
        }
    }
}