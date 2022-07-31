using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttackSpeed : DeBuff
{
    float speedDown = 0;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="speedDown">减速的速度</param>
    /// <param name="time">减速时间</param>
    public SubAttackSpeed(float speedDown, float time)
    {
        this.speedDown = speedDown;
        LastTime = time;
    }

    override public void Apply(CharacterInfo info)
    {
        //info.attackSpeed -= this.speedDown;
        info.attackSpeed = Mathf.Max(info.attackSpeed - speedDown, 0);
    }
}