using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    [SerializeField]
    private CharacterInfo templateData;
    private CharacterInfo characterData;
    private CharacterInfo realData; // 实际数据
    private BuffController buffController = new BuffController();

    // TODO: 用 PermissionController 控制技能
    // private PermissionController permissionController = new PermissionController();

    // public Skill skill;
    // public string skill_name;

    private float tempDefence;
    private float tempSpeed;

    private void Awake()
    {
        if (templateData != null)
        {
            characterData = Instantiate(templateData);
            realData = Instantiate(templateData);
        }

        tempDefence = characterData.defence;
        tempSpeed = characterData.speed;
        // skill = null;
    }

    private void Update()
    {
        BuffController();
    }

    private void BuffController()
    {
        CharacterInfo temp = Instantiate(characterData);
        buffController.ApplyBuffs(temp);
        realData = temp;
    }

    public void AddBuff(Buff buff)
    {
        buffController.AddBuff(buff);
    }

    #region 数据获取
    public float HackDis { get { return realData.hackDis; } }
    public float Speed { get { return realData.speed; } }
    public float AttackSpeed { get { return realData.attackSpeed; } }
    public float ShootSpeed { get { return realData.shootSpeed; } }
    public float MaxHealth { get { return realData.maxHealth; } }
    public float Health { get { return realData.health; } }
    public float SpeedRatio { get { return realData.speedRatio; } }
    public float Energy { get { return realData.energy; } }
    public float MaxEnergy { get { return realData.maxEnergy; } }
    public bool CanShoot { get { return realData.canShoot; } set { characterData.canShoot = value; } }
    public String CharacterTag { get { return realData.characterTag; } }
    public float Jumpforce { get { return realData.jumpforce; } }
    public float ChangeStateEnergy { get { return realData.changeStateEnergy; } }
    public float ChangeStateTime { get { return realData.changeStateTime; } }
    public bool IsStagger { get { return realData.isStagger; } }
    #endregion

    private void AddEnergy(float x)
    {
        characterData.energy = Mathf.Min(characterData.maxEnergy, characterData.energy + x);
    }

    public void SubEnergy(float x)
    {
        characterData.energy = Mathf.Max(0, characterData.energy - x);
    }

    private void AddHealth(float x)
    {
        characterData.health = Mathf.Min(characterData.maxHealth, characterData.health + x);
    }

    private void SubHealth(float x)
    {
        characterData.health = Mathf.Max(0, characterData.health - x);
    }

    private void AddSpeed(float x)
    {
        characterData.speed += x;
    }

    private void SubSpeed(float x)
    {
        characterData.speed = Mathf.Max(0, characterData.speed - x);
    }

    private void ResumeSpeed()
    {
        characterData.speed = tempSpeed;
    }

    private void AddDefence(float x)
    {
        characterData.defence += x;
    }

    private void SubDefence(float x)
    {
        characterData.defence = Mathf.Max(0, characterData.defence - x);
    }

    private void ResumeDefence()
    {
        characterData.defence = tempDefence;
    }

    public void TakeDamage(InfoController attacker, InfoController defender)
    {
        //被攻击者血量减少
        float damage = attacker.characterData.damage - defender.characterData.defence;
        damage = damage <= 0 ? 0 : damage;
        defender.SubHealth(damage);
        //攻击者获取能量
        attacker.AddEnergy(attacker.characterData.energyAttack);
    }

    public void TakeDamageBySkill(InfoController attacker, InfoController defender)
    {
        //被攻击者血量减少
        float damage = attacker.characterData.damage - defender.characterData.defence;
        damage = damage <= 0 ? 0 : damage;
        defender.SubHealth(damage);
    }

    public void LossEnergy(float deltaTime)
    {
        SubEnergy(characterData.energyLoss * deltaTime);
    }

    public void GetEnergy(float deltaTime)
    {
        AddEnergy(characterData.energyGet * deltaTime);
    }

    public bool isFallDown()
    {
        // test
        return characterData.health <= 0;
    }

    public void Resume(float resumeHealth)
    {
        AddHealth(resumeHealth);
    }

    public void DefenceBuff(float defence)
    {
        AddDefence(defence);
    }

    public void DefenceDebuff(float defence)
    {
        SubDefence(defence);
    }

    // public void DefenceResume()
    // {
    //     ResumeDefence();
    // }

    public void SpeedBuff(float speed)
    {
        AddSpeed(speed);
    }

    public void SpeedDebuff(float speed)
    {
        SubSpeed(speed);
    }

    // public void SpeedResume()
    // {
    //     ResumeSpeed();
    // }
}