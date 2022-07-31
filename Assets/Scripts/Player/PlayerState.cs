using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Transform transform;

    protected InfoController pInfo;

    protected PlayerState(Transform transform, InfoController pInfo)
    {
        this.transform = transform;
        this.pInfo = pInfo;
    }

    protected PlayerState(Transform transform)
    {
        this.transform = transform;
    }

    // note: 离开该形态的时候要调用此方法
    public abstract void Leave();

    // note: 转变形态的时候要 reset 一下
    public abstract void Reset();

    public abstract void Update();

    public abstract void FixedUpdate();
}
