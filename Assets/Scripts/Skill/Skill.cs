using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    protected int level;
    public float coolDown;
    public int times;
    public float initialCoolDown;
    public bool isFirst;
    protected Transform transform;

    protected Skill(Transform transform)
    {
        this.transform = transform;
        this.times = 3;
        this.initialCoolDown = 1f;
        this.isFirst = true;
    }

    protected void GetCoolDown()
    {
        switch (this.level)
        {
            case 1:
                this.coolDown = 4f;
                break;
            case 2:
                this.coolDown = 7f;
                break;
            case 3:
                this.coolDown = 15f;
                break;
        }
    }

    public abstract void SkillActive();
}
