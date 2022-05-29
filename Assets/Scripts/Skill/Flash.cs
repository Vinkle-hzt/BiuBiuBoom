using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flash : Skill
{
    //位移距离
    private float moveDistance;
    //位移时间
    private float moveTime;
    //增加的移速
    private float speedUp;
    //加速时长
    private float speedUpTime;

    public Flash(Transform transform, float moveDistance, float speedUp) : base(transform)
    {
        base.level = 2;
        base.GetCoolDown();
        this.moveDistance = moveDistance;
        this.speedUp = speedUp;
        speedUpTime = 5;
    }

    public override void SkillActive()
    {
        base.times--;

        //人物图片消失
        transform.Find("Body").GetComponent<SpriteRenderer>().enabled = false;

        Vector3 forward = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        Vector3 moveTowards = forward * moveDistance;
        Vector3 destination = moveTowards + transform.position;

        transform.gameObject.layer = LayerMask.NameToLayer("Shadow");
        transform.position = destination;
        transform.gameObject.layer = LayerMask.NameToLayer("Default");

        //重启人物图片
        transform.Find("Body").GetComponent<SpriteRenderer>().enabled = true;

        transform.Find("BuffController").GetComponent<BuffController>().StartSpeedUp(speedUp, speedUpTime);
    }

}