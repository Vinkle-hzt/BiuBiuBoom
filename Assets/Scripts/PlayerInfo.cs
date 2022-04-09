using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 人物信息类，存储一些人物的信息
/// </summary>
public class PlayerInfo : MonoBehaviour
{
    [Header("最大生命值")]
    public float maxHealth;

    [Header("血量")]
    public float health;

    [Header("最大能量")]
    public float maxEnergy;

    [Header("能量")]
    public float energy;

    [Header("移速")]
    public float speed;

    [Header("跳跃能力")]
    public float jumpforce;

    [Header("攻速")]
    public float attackSpeed;

    [Header("子弹速度")]
    public float shootSpeed;

    public void AddEnergy(float x)
    {
        energy = Mathf.Min(maxEnergy, energy + x);
    }

    public void SubEnergy(float x)
    {
        energy = Mathf.Max(0, energy - x);
    }

    public void AddHealth(float x)
    {
        health = Mathf.Min(maxHealth, health + x);
    }

    public void SubHealth(float x)
    {
        health = Mathf.Max(0, health - x);
    }
}
