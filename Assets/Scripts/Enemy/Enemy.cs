﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BiuBiuBoom.Utils;

public class Enemy : MonoBehaviour
{
    // TODO: 更改EnemyState相关的代码--增加了control
    public enum EnemyState
    {
        normal, fallDown, control
    }

    public InfoController eInfo;
    private EnemyInfoController enemyInfo;
    private bool isFallDown;

    private float curTime;
    private EnemyState state;

    private void Awake()
    {
        eInfo = GetComponent<InfoController>();
        enemyInfo = GetComponent<EnemyInfoController>();

        //正常状态
        state = EnemyState.normal;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FallDown();
        (state == EnemyState.normal ? (Action)NormalMovement : FallDownAttack)();
        (state == EnemyState.normal ? (Action)NormalAttack : FallDownAttack)();
    }

    #region 被控制
    public void Control(float time)
    {
        state = EnemyState.control;
        Vector3 position = new Vector3();
        position.x = Input.GetAxis("Horizontal");
        transform.position += position * eInfo.Speed * time;
    }
    #endregion

    #region 死亡
    public void Dead()
    {
        // 死亡动画
        Destroy(gameObject);
    }
    #endregion

    #region 移动
    void NormalMovement()
    {
        //AI移动
        //不同敌人有不同的移动方法，此处为父类，实现为空方法
    }

    void FallDownMovement()
    {
        //TODO:瘫痪统一不能移动
    }
    #endregion

    #region 攻击
    void NormalAttack()
    {
        //同移动为空方法
    }

    void FallDownAttack()
    {
        //同瘫痪移动
    }
    #endregion

    #region 瘫痪与重启
    //瘫痪
    void FallDown()
    {
        if (eInfo.isFallDown())
        {
            if (!isFallDown)
            {
                curTime = enemyInfo.characterData.fallDownTime;
                isFallDown = true;
                state = EnemyState.fallDown;
                // 更改 layer(用于实现斩杀和骇入)
                UtilsClass.ChangeLayer(transform, LayerMask.NameToLayer("EnemyFall"));
                //TODO: 瘫痪的美术表现
                //用枪消失替代美术表现
                transform.Find("Aim").gameObject.SetActive(false);
            }

            if (curTime <= 0)
            {
                isFallDown = false;
                state = EnemyState.normal;
                //重启
                Resume();
            }
            else
            {
                curTime -= Time.deltaTime;
            }
        }
    }

    void Resume()
    {
        //用枪消失替代美术表现
        transform.Find("Aim").gameObject.SetActive(true);
        // 更改 layer(用于实现斩杀和骇入)
        UtilsClass.ChangeLayer(transform, LayerMask.NameToLayer("Enemy"));
        enemyInfo.Resume();
        eInfo.Resume(eInfo.characterData.maxHealth * enemyInfo.characterData.resumePercent);
    }
    #endregion
}