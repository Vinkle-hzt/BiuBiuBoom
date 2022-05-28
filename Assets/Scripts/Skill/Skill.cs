using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    protected int level;
    protected float coolDown;
    protected int times;
    protected float initialCoolDown;
    protected Transform transform;

    protected Skill(Transform transform)
    {
        this.transform = transform;
        this.times = 3;
        this.initialCoolDown = 1f;
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
