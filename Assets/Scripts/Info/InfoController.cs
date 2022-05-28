using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    public CharacterInfo templateData;
    public CharacterInfo characterData;

    public float HackDis { get { return characterData.hackDis; } }
    public float Speed { get { return characterData.speed; } }
    public float AttackSpeed { get { return characterData.attackSpeed; } }
    public float ShootSpeed { get { return characterData.shootSpeed; } }
    private void Awake()
    {
        if (templateData != null)
        {
            //生成的方法
            characterData = Instantiate(templateData);
        }
    }

    private void AddEnergy(float x)
    {
        characterData.energy = Mathf.Min(characterData.maxEnergy, characterData.energy + x);
    }

    private void SubEnergy(float x)
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

    }

    private void SubSpeed(float x)
    {

    }

    private void AddDefence(float x)
    {

    }

    private void SubDefence(float x)
    {

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

    public void SpeedBuff(float speed)
    {
        AddSpeed(speed);
    }

    public void SpeedDebuff(float speed)
    {
        SubSpeed(speed);
    }
}