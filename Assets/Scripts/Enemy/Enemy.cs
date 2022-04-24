using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        normal, fallDown
    }

    private InfoController eInfo;
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
        enemyInfo.Rusume();
        eInfo.Resume(eInfo.characterData.maxHealth * enemyInfo.characterData.resumePercent);
    }
    #endregion
}
