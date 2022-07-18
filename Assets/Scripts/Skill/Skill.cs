using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    protected int level;
    public float cache;
    public float coolDownRatio;
    protected Transform transform;

    protected Skill(Transform transform)
    {
        this.transform = transform;
    }

    protected void InitialCoolDownRatio()
    {
        switch (this.level)
        {
            case 1:
                this.coolDownRatio = 0.06f;
                break;
            case 2:
                this.coolDownRatio = 0.1f;
                break;
            case 3:
                this.coolDownRatio = 0.18f;
                break;
        }
    }

    protected void InitialCache()
    {
        switch (this.level)
        {
            case 1:
                this.cache = 8f;
                break;
            case 2:
                this.cache = 12f;
                break;
            case 3:
                this.cache = 20f;
                break;
        }
    }

    public abstract void SkillActive();
}
