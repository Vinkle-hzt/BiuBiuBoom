using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController
{
    ArrayList buffs = new ArrayList();

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
    }

    /// <summary>
    /// 清除 buff
    /// </summary>
    /// <param name="buff"></param>
    public void RemoveBuff(Buff buff)
    {
        buffs.Remove(buff);
    }

    /// <summary>
    /// 应用 buff
    /// </summary>
    /// <param name="info"></param>
    public void ApplyBuffs(CharacterInfo info)
    {
        // 清除无效的 buff
        clearTimeOutBuff();

        foreach (Buff buff in buffs)
        {
            buff.AddTime(Time.deltaTime);
            buff.Apply(info);
        }
    }

    /// <summary>
    /// 清除Debuff
    /// </summary>
    public void clearDebuff()
    {
        ArrayList debuffs = new ArrayList();

        foreach (Buff buff in buffs)
            if (buff is DeBuff)
                debuffs.Add(buff);

        foreach (Buff buff in debuffs)
            buffs.Remove(buff);
    }
    void clearTimeOutBuff()
    {
        ArrayList timeOutBuffs = new ArrayList();

        foreach (Buff buff in buffs)
            if (buff.BuffTimeOut())
                timeOutBuffs.Add(buff);

        foreach (Buff buff in timeOutBuffs)
            buffs.Remove(buff);
    }
}
