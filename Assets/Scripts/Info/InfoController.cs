using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    public CharacterInfo templateData;
    public CharacterInfo characterData;

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
        Debug.Log(defender.characterData.health);
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

    bool isFallDown()
    {
        // test
        return characterData.health <= characterData.health * characterData.perFallDown;
    }
}