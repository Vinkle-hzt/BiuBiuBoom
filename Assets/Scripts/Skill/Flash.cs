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

        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 cursorPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 forward = (cursorPos - playerPos).normalized;
        Vector2 moveTowards = forward * moveDistance;
        Vector2 destination = moveTowards + playerPos;

        transform.position = destination;

        //重启人物图片
        transform.Find("Body").GetComponent<SpriteRenderer>().enabled = true;

        transform.Find("BuffController").GetComponent<BuffController>().StartSpeedUp(speedUp, speedUpTime);
    }

}