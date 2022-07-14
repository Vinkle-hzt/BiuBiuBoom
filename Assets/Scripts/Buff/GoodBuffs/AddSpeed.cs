using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpeed : GoodBuff
{
    float speedUp = 0;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="speedUp">加速的速度</param>
    /// <param name="time">加速时间</param>
    public AddSpeed(float speedUp, float time)
    {
        this.speedUp = speedUp;
        LastTime = time;
    }

    override public void Apply(CharacterInfo info)
    {
        info.attackSpeed += this.speedUp;
    }
}
