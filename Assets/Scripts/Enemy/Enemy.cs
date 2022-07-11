using System.Collections;
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
    public EnemyInfoController enemyInfo;
    private bool isFallDown;
    private float curTime;
    protected EnemyState state;
    private SpriteRenderer spriteRenderer;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        eInfo = GetComponent<InfoController>();
        enemyInfo = GetComponent<EnemyInfoController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //正常状态
        state = EnemyState.normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (state != EnemyState.control)
            {
                FallDown();
                (state == EnemyState.normal ? (Action)NormalMovement : FallDownAttack)();
                (state == EnemyState.normal ? (Action)NormalAttack : FallDownAttack)();
            }
        }
    }

    public virtual String GetSkill()
    {
        return null;
    }

    #region 被控制
    public virtual void Control(float time)
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
        isDead = true;
        Destroy(gameObject);
    }

    virtual protected void DeadAnim()
    {

    }
    #endregion

    #region 移动
    virtual protected void NormalMovement()
    {
        //AI移动
        //不同敌人有不同的移动方法，此处为父类，实现为空方法
    }

    virtual protected void FallDownMovement()
    {
        //TODO:瘫痪统一不能移动
    }
    #endregion

    #region 攻击
    virtual protected void NormalAttack()
    {
        //同移动为空方法
    }

    virtual protected void FallDownAttack()
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
                spriteRenderer.color = UtilsClass.HexToColor("FF8A8AFF");
                BgmManager.instance.PlayEnemyFallDown();
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

    protected Transform findTarget()
    {
        var cur = Physics2D.OverlapCircle(transform.position, enemyInfo.PerceptionDis, LayerMask.GetMask("Player"));
        return cur ? cur.transform : null;
    }

    void Resume()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        // 更改 layer(用于实现斩杀和骇入)
        UtilsClass.ChangeLayer(transform, LayerMask.NameToLayer("Enemy"));
        enemyInfo.Resume();
        eInfo.Resume(eInfo.MaxHealth * enemyInfo.characterData.resumePercent);
    }
    #endregion
}