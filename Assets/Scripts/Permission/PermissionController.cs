﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PermissionController : MonoBehaviour
{
    [Header("技能区")]
    public string[] skills;
    public int skillNums;
    [Header("当前技能")]
    public int curSkillIndex;
    [Header("缓冲区")]
    public string cacheSkill;

    [Header("交换技能")]
    public float exchangeCoolDownTime;
    private float curExchangeCoolDownTime;

    [Header("缓存")]
    public float cache;
    public float maxCache;
    private float[] skillCache;

    [Header("CD")]
    public float curCoolDownTime;
    public float maxCoolDownTime;

    private Skill curSkill;
    public bool isOverload;

    private InfoController pInfo;

    // [Header("")]

    // Start is called before the first frame update
    void Start()
    {
        skills = new string[skillNums];
        skillCache = new float[skillNums];
        curSkillIndex = 0;
        cacheSkill = null;
        cache = 0;
        isOverload = false;
        curExchangeCoolDownTime = exchangeCoolDownTime;
        maxCoolDownTime = 0;
        pInfo = GetComponent<InfoController>();

        for (int i = 0; i < skillNums; i++)
        {
            //skills[i] = "InformationBombing";
            skills[i] = "RCE";
        }
        InitialCurrentSkill();
    }

    // Update is called once per frame
    void Update()
    {
        ExchangeCurSkill();
        if (!isOverload)
        {
            UseSkill();
        }
        TotalCache();
        CheckOverload();
    }

    public void GetNewSkill(string skillName)
    {
        // if (count < skillNums && count >= 0)
        // {
        //     //技能数量没满
        //     //添加技能
        //     if (count == 0)
        //     {
        //         curSkillIndex = 0;
        //     }
        //     skills[count] = skillName;
        //     count++;
        //     InitialCurrentSkill();
        // }
        // else
        // {
        //技能满了
        cacheSkill = skillName;
        // }
    }

    public void ExchangeCurSkill()
    {
        if (cacheSkill != null)
        {
            if (Input.GetKey(InputController.instance.exchange))
            {
                if (curExchangeCoolDownTime <= 0)
                {
                    curExchangeCoolDownTime = exchangeCoolDownTime;
                    //交换技能
                    skills[curSkillIndex] = cacheSkill;
                    cacheSkill = null;
                    //初始化技能
                    InitialCurrentSkill();
                    //清理缓存
                    skillCache[curSkillIndex] = 0f;
                }
                else
                {
                    curExchangeCoolDownTime -= Time.deltaTime;
                }
            }
        }
    }

    public void UseSkill()
    {
        //触发技能

        if (curCoolDownTime <= 0)
        {
            if (pInfo.CanShoot && Input.GetKeyDown(InputController.instance.skill))
            {
                //增加缓存
                skillCache[curSkillIndex] += curSkill.cache;

                curSkill.SkillActive();
                curSkillIndex = (curSkillIndex + 1) % skillNums;
                InitialCurrentSkill();

                //进入CD
                maxCoolDownTime = cache * curSkill.coolDownRatio;
                curCoolDownTime = maxCoolDownTime;
            }
        }
        else
        {
            curCoolDownTime -= Time.deltaTime;
        }
    }

    private void InitialCurrentSkill()
    {
        string skillName = skills[curSkillIndex];
        switch (skillName)
        {
            case "RCE":
                curSkill = new RCE(transform);
                break;
            case "Flash":
                curSkill = new Flash(transform, 5f, 3f);
                break;
            case "InformationBombing":
                curSkill = new InformationBombing(transform);
                break;
            case "TrafficHijacking":
                curSkill = new TrafficHijacking(transform);
                break;
            case "ForceAuthorization":
                curSkill = new ForceAuthorization(transform);
                break;
            default:
                break;
        }
    }

    void TotalCache()
    {
        float total = 0f;
        for (int i = 0; i < skillNums; i++)
        {
            total += skillCache[i];
        }
        cache = total;
    }

    void CheckOverload()
    {
        if (cache >= maxCache)
        {
            isOverload = true;
        }
        else
        {
            isOverload = false;
        }
    }
}
