using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 人物信息类，存储一些人物的信息
/// </summary>
[CreateAssetMenu(fileName = "CharacterInfo", menuName = "BiuBiuBoom/CharacterInfo", order = 0)]
public class CharacterInfo : ScriptableObject
{
    [Header("Tag")]
    public string characterTag;

    [Header("最大生命值")]
    public float maxHealth;

    [Header("血量")]
    public float health;

    [Header("防御力")]
    public float defence;

    [Header("最大能量")]
    public float maxEnergy;

    [Header("能量")]
    public float energy;

    [Header("攻击获取能量")]
    public float energyAttack;

    [Header("能量损失速度")]
    public float energyLoss;

    [Header("能量恢复速度")]
    public float energyGet;

    [Header("变身所需要的能量")]
    public float changeStateEnergy;

    [Header("变身冷却时间")]
    public float changeStateTime;

    [Header("移速")]
    public float speed;

    [Header("移速倍率")]
    public float speedRatio;

    [Header("跳跃能力")]
    public float jumpforce;

    [Header("攻速")]
    public float attackSpeed;

    [Header("子弹速度")]
    public float shootSpeed;

    [Header("子弹伤害")]
    public float damage;

    [Header("能否射击")]
    public bool canShoot = true;

    [Header("斩杀和骇入半径")]
    public float hackDis;
}
