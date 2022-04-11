using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Transform transform;

    protected PlayerInfo pInfo;

    protected State(Transform transform, PlayerInfo pInfo) 
    { 
        this.transform = transform;
        this.pInfo = pInfo;
    }

    protected State(Transform transform)
    {
        this.transform = transform;
    }
    // note: 转变形态的时候要 reset 一下
    public abstract void Reset();

    public abstract void Update();

    public abstract void FixedUpdate();
}
