using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSpeed : DeBuff
{
    float speedDown = 0;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="defenceDown">减少的移速</param>
    /// <param name="time">减速时间</param>
    public SubSpeed(float speedDown, float time)
    {
        this.speedDown = speedDown;
        LastTime = time;
    }

    override public void Apply(CharacterInfo info)
    {
        //info.speed -= this.speedDown;
        info.speed = Mathf.Max(info.speed - this.speedDown, 0);
    }
}
