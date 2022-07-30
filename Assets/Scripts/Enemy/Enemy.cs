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

    public bool isElite;   //是否是高级怪
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
                if (state == EnemyState.normal)
                {
                    // 检查是否还在硬直状态
                    if (eInfo.IsStagger)
                        Stagger();
                    else
                    {
                        NormalMovement();
                        NormalAttack();
                    }
                }
                else
                {
                    FallDownAttack();
                    FallDownAttack();
                }
            }
        }
    }

    public virtual String GetSkill()
    {
        int skillLevel = UnityEngine.Random.Range(0, 100) + 1;
        if (isElite)
        {
            if (skillLevel >= 1 && skillLevel <= 20)
            {
                SkillLevelOne skill = new SkillLevelOne();
                int skillName = UnityEngine.Random.Range(0, System.Enum.GetNames(skill.GetType()).Length);
                return ((SkillLevelOne)skillName).ToString();
            }
            else if (skillLevel >= 21 && skillLevel <= 50)
            {
                SkillLevelTwo skill = new SkillLevelTwo();
                int skillName = UnityEngine.Random.Range(0, System.Enum.GetNames(skill.GetType()).Length);
                return ((SkillLevelTwo)skillName).ToString();
            }
            else if (skillLevel >= 51 && skillLevel <= 100)
            {
                SkillLevelThree skill = new SkillLevelThree();
                int skillName = UnityEngine.Random.Range(0, System.Enum.GetNames(skill.GetType()).Length);
                return ((SkillLevelThree)skillName).ToString();
            }
        }
        else
        {
            if (skillLevel >= 1 && skillLevel <= 70)
            {
                SkillLevelOne skill = new SkillLevelOne();
                int skillName = UnityEngine.Random.Range(0, System.Enum.GetNames(skill.GetType()).Length);
                return ((SkillLevelOne)skillName).ToString();
            }
            else if (skillLevel >= 71 && skillLevel <= 100)
            {
                SkillLevelTwo skill = new SkillLevelTwo();
                int skillName = UnityEngine.Random.Range(0, System.Enum.GetNames(skill.GetType()).Length);
                return ((SkillLevelTwo)skillName).ToString();
            }
        }
        return null;
    }

    #region 硬直状态
    protected virtual void Stagger()
    {

    }
    #endregion

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
        EventHandler.CallEnemyDeathEvent();
        Destroy(gameObject);
    }

    virtual protected void DeadAnim()
    {

    }
    #endregion

    #region 移动
    virtual protected void NormalMovement()
    {
    }

    virtual protected void FallDownMovement()
    {
    }
    #endregion

    #region 攻击
    virtual protected void NormalAttack()
    {
    }

    virtual protected void FallDownAttack()
    {
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
                UtilsClass.ChangeLayer(transform, LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("EnemyFall"));
                // 更改颜色表示瘫痪
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
        UtilsClass.ChangeLayer(transform, LayerMask.NameToLayer("EnemyFall"), LayerMask.NameToLayer("Enemy"));
        enemyInfo.Resume();
        eInfo.Resume(eInfo.MaxHealth * enemyInfo.characterData.resumePercent);
    }
    #endregion
}