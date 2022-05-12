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

    public void TakeDamage(InfoController attacker, InfoController defender)
    {
        //被攻击者血量减少
        defender.SubHealth(attacker.characterData.damage);
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
}